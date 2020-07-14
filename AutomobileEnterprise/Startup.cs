using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomobileEnterprise.Startup))]
namespace AutomobileEnterprise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
