using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(insertData.Startup))]
namespace insertData
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
