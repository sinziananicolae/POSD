using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POSD_Tema1.Startup))]
namespace POSD_Tema1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
