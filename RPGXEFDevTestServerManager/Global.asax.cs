using System.Configuration;
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
using RPGXEFDevTestServerManager.ExternalHelpers;
using RPGXEFDevTestServerManager.Models;

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

            // Register your types, for instance:
            container.RegisterPerWebRequest(() => new GitCommandBuilder(ConfigurationManager.AppSettings["rpgxefsrcpath"]));
            container.RegisterPerWebRequest(() => new SshHelper("vagrant", ConfigurationManager.AppSettings["privatekeypath"], "127.0.0.1", 2222));
            container.RegisterPerWebRequest<ApplicationDbContext>();
            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()));
            container.RegisterPerWebRequest(() => HttpContext.Current.GetOwinContext().Authentication);
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();

            // Register all Controllers
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // ???
            container.RegisterMvcIntegratedFilterProvider();

            // Can't verify because of lack of HttpContext
            //container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
