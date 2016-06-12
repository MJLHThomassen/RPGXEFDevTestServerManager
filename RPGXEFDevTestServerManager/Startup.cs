using Microsoft.Owin;
using Owin;
using RPGXEFDevTestServerManager;

[assembly: OwinStartup(typeof(Startup))]
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
