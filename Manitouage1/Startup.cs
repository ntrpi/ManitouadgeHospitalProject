using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Manitouadge.Startup))]
namespace Manitouadge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
