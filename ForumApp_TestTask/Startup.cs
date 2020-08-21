using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForumApp_TestTask.Startup))]
namespace ForumApp_TestTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
