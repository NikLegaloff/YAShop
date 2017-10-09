using Microsoft.Owin;
using Owin;
using Sprut.StoreAdmin;

[assembly: OwinStartup(typeof(Startup))]

namespace Sprut.StoreAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}