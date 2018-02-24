using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ecommerce01.Startup))]
namespace Ecommerce01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
