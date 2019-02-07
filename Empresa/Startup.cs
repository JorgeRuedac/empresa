using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Empresa.Startup))]
namespace Empresa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
 
