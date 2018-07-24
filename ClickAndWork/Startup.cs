using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClickAndWork.Startup))]
namespace ClickAndWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
