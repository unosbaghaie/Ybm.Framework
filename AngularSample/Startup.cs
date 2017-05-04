using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularSample.Startup))]
namespace AngularSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
