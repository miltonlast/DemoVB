using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Store.Model;
using System.Linq.Dynamic;
using Northwind.Store.Notification;

namespace Northwind.Store.Data
{
    public class RegionD : DataBase<Region>, IMaintenanceD<Region, int>
    {
        public Region Read(int id)
        {
            return db.Regions.Find(id);
        }

        public Region Read(string description)
        {
            Region result = null;

            result = db.Regions.
                Where(c => c.RegionDescription.Equals(description,
                    StringComparison.CurrentCultureIgnoreCase)).
                    SingleOrDefault();

            return result;
        }

        public List<Region> ReadList(PageFilter pf = null)
        {
            List<Region> result = new List<Region>();

            if (pf != null)
            {
                pf.Count = db.Regions.Count();
                result = db.Regions.OrderBy(pf.Sorting).
                    Skip(--pf.Page * pf.PageSize).
                    Take(pf.PageSize).ToList();
            }
            else
            {
                result = db.Regions.ToList();
            }

            return result;
        }

        public void Delete(int id)
        {
            db.Database.ExecuteSqlCommand(
                "delete from region where regionid = @RegionID",
                new System.Data.SqlClient.SqlParameter(
                    "@RegionID", System.Data.SqlDbType.Int) { Value = id });
        }
    }
}
