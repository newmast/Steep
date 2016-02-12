using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Steep.Web.Startup))]
namespace Steep.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
