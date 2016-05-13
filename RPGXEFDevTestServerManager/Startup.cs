using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPGXEFDevTestServerManager.Startup))]
namespace RPGXEFDevTestServerManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
