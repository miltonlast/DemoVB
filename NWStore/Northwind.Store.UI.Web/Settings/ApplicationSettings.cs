using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Store.UI.Web.Settings
{
    public class ApplicationSettings
    {
        public static string WelcomeMessage
        {
            get
            {
                return (string)HttpContext.Current.Application["WelcomeMessage"];
            }
            set
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["WelcomeMessage"] = value;
                HttpContext.Current.Application.UnLock();
            }
        }  
    }
}