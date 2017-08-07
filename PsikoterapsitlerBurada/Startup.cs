using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PsikoterapsitlerBurada.Startup))]
namespace PsikoterapsitlerBurada
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
