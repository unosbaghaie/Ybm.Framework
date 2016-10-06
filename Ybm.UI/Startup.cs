using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ybm.UI.Startup))]
namespace Ybm.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
