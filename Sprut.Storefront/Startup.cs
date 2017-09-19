using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sprut.Storefront.Startup))]
namespace Sprut.Storefront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
