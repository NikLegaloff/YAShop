using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YASop.AdminUI.Startup))]
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
