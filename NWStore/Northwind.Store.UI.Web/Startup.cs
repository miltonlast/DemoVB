using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Northwind.Store.UI.Web.Startup))]
namespace Northwind.Store.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
