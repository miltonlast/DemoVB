using System.Web;
using System.Web.Mvc;

namespace Northwind.Store.UI.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Aplica Authorize para todos los controllers
            //filters.Add(new AuthorizeAttribute());
            //filters.Add(new AllowAnonymousAttribute());

            filters.Add(new HandleErrorAttribute());
        }
    }
}
