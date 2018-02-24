using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_web_no_Net_Core.Startup))]
namespace MVC_web_no_Net_Core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
