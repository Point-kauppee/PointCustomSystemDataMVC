using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PointCustomSystemDataMVC.Startup))]
namespace PointCustomSystemDataMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
