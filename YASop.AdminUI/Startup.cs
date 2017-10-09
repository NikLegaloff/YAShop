using Microsoft.Owin;
using Owin;
using YASop.AdminUI;

[assembly: OwinStartup(typeof(Startup))]

namespace YASop.AdminUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}