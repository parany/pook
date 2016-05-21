using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pook.Data.Startup))]
namespace Pook.Data
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
