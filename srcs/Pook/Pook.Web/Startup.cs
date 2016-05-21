using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pook.Web.Startup))]
namespace Pook.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
