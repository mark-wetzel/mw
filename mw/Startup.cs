using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mw.Startup))]
namespace mw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
