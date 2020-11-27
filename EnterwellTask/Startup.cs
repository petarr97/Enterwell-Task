using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnterwellTask.Startup))]
namespace EnterwellTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
