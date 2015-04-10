using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArticleSystem.Web.Startup))]
namespace ArticleSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
