using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Store.UI.Web.Settings
{
    public class SessionSettings
    {
        public static string Message
        {
            get
            {
                return (string)HttpContext.Current.Session["Message"];
            }
            set
            {
                HttpContext.Current.Session["Message"] = value;
            }
        }

        //public static ViewModels.ShoppingCart Cart
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["Cart"] == null)
        //        {
        //            HttpContext.Current.Session["Cart"] = new ViewModels.ShoppingCart();
        //        }

        //        return (ViewModels.ShoppingCart)HttpContext.Current.Session["Cart"];
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["Cart"] = value;
        //    }
        //}
    }
}