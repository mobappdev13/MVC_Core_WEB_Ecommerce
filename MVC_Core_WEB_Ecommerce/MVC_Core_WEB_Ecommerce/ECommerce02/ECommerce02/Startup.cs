using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECommerce02.Startup))]
namespace ECommerce02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
