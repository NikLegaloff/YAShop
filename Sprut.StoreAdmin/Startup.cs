using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sprut.StoreAdmin.Startup))]
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
