using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoardCockpit.Startup))]
namespace BoardCockpit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
