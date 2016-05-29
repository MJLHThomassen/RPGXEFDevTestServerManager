using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using WithMartin.GitCommandBuilder;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RPGXEFDevTestServerManager.Controllers;
using RPGXEFDevTestServerManager.ExternalHelpers;
using RPGXEFDevTestServerManager.Models;
using WithMartin.GitCommandBuilder.FluentApi;

namespace RPGXEFDevTestServerManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureDI();
        }

        private void ConfigureDI()
        {
            // Create the container.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Get Paths
            var webRootPath = Server.MapPath("~");
            var realtiveRpgxEfSrcPath = ConfigurationManager.AppSettings["relative_rpgxefsrc_path"];
            var relativeRpgxEfBuildHistoryPath = ConfigurationManager.AppSettings["relative_rpgxefbuildhistory_path"];
            var privateKeyPath = ConfigurationManager.AppSettings["privatekey_path"];

            var physicalRpgxEfSrcPath = Path.GetFullPath(Path.Combine(webRootPath, realtiveRpgxEfSrcPath));
            var physicalRpgxEfBuildHistoryPath = Path.GetFullPath(Path.Combine(webRootPath, relativeRpgxEfBuildHistoryPath));
            var physicalPrivateKeyPath = Environment.ExpandEnvironmentVariables(privateKeyPath);

            container.RegisterPerWebRequest(() => new GitCommandBuilder(physicalRpgxEfSrcPath));
            container.RegisterPerWebRequest(() => new SshHelper("vagrant", physicalPrivateKeyPath, "127.0.0.1", 2222));

            // Register Database stuff
            container.RegisterPerWebRequest<ApplicationDbContext>();
            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()));
            container.RegisterPerWebRequest(() => HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();

            // Register all Controllers
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Options.AllowOverridingRegistrations = true;
            container.RegisterPerWebRequest(() => new HomeController(container.GetInstance<GitHelper>(), container.GetInstance<SshHelper>(), physicalRpgxEfBuildHistoryPath));
            container.Options.AllowOverridingRegistrations = false;

            // ???
            container.RegisterMvcIntegratedFilterProvider();

            // Can't verify because of lack of HttpContext
            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            ConfigureRPGXEFSrc(container);
        }

        private void ConfigureRPGXEFSrc(Container container)
        {
            var gitBuilder = new GitCommandBuilder("");
            var ssh = container.GetInstance<SshHelper>();

            try
            {
                // Clone on the vm because of .git directory access rights
                // TODO: Find out why '.git': Not a Directory when not doing this on the vm
                var gitCommand = gitBuilder.Clone().Repository("https://github.com/solarisstar/rpgxEF.git").Directory("/vagrant/RPGXEFSrc");
                var command = gitCommand.ToString();
                ssh.Run(command);
            }
            catch
            {
                // Already cloned
            }
        }
    }
}
