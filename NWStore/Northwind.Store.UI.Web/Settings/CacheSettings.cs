using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Northwind.Store.UI.Web.Settings
{
    public class CacheSettings
    {
        public static string Offer
        {
            get { return (string)HttpContext.Current.Cache["Offer"]; }
            set
            {
                HttpContext.Current.Cache.Add("Offer",
                    value,
                    null,
                    Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 0, 30),
                    System.Web.Caching.CacheItemPriority.Default,
                    new CacheItemRemovedCallback(RemovedCallback));

                System.Diagnostics.Debug.WriteLine("Cache iniciado " + 
                    DateTime.Now);
            }
        }

        static void RemovedCallback(String k, Object v, 
            CacheItemRemovedReason r)
        {
            string detalle = string.Format("{0} {1} {2}",
                k, v, r);

            System.Diagnostics.Debug.WriteLine(detalle);
            System.Diagnostics.Debug.WriteLine("Cache expirado " + 
                DateTime.Now);
        }
    }
}