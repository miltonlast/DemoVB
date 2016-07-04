using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Northwind.Store.UI.Web.Settings
{
    public class RequestSettings
    {
        Controller controller = null;
        public RequestSettings(Controller c)
        {
            controller = c;
        }

        public Store.Notification.Message Message
        {
            get
            {
                return (Store.Notification.Message)controller.TempData["Message"];
            }
            set
            {
                controller.TempData["Message"] = value;
            }
        } 
    }
}