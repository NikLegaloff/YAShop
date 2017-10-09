using Microsoft.Owin;
using Owin;
using Sprut.Storefront;

[assembly: OwinStartup(typeof(Startup))]

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