using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ybm.UI.Angular.Startup))]
namespace Ybm.UI.Angular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
