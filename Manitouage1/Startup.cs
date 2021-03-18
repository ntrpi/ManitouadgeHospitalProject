using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Manitouage1.Startup))]
namespace Manitouage1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
