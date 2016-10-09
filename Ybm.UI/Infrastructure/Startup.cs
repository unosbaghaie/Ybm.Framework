using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ybm.UI.Infrastructure.Startup))]
namespace Ybm.UI.Infrastructure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            Ybm.Framework.Configuration.Configure.StartupConfiguration(app);
        }
        public void ConfigureAuth()
        {

        }
    }
}
