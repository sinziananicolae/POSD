using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tema1.Startup))]
namespace Tema1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
