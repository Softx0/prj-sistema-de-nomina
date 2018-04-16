using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SISTEMANOMINA.Startup))]
namespace SISTEMANOMINA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
