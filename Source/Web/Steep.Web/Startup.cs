using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(Steep.Web.Startup))]

namespace Steep.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
