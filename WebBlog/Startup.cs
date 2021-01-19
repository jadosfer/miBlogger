using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBlog.Startup))]
namespace WebBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
