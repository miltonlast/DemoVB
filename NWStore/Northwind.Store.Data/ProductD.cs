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
    public class ProductD : DataBase<Product>, IMaintenanceD<Product, int>
    {
        public Product Read(int id)
        {
            return db.Products.Include("Category").Include("Supplier").SingleOrDefault(p => p.ProductID == id);
        }

        public Product Read(string name)
        {
            Product result = null;

            result = db.Products.Include("Category").Include("Supplier").
                Where(c => c.ProductName.Equals(name,
                    StringComparison.CurrentCultureIgnoreCase)).
                    SingleOrDefault();

            return result;
        }

        public List<Product> ReadList(PageFilter pf = null)
        {
            List<Product> result = new List<Product>();

            if (pf != null)
            {
                pf.Count = db.Products.Count();
                result = db.Products.OrderBy(pf.Sorting).
                    Skip(--pf.Page * pf.PageSize).
                    Take(pf.PageSize).ToList();
            }
            else
            {
                result = db.Products.ToList();
            }

            return result;
        }

        public void Delete(int id)
        {
            db.Database.ExecuteSqlCommand(
                "delete from products where productid = @ProductID",
                new System.Data.SqlClient.SqlParameter(
                    "@ProductID", System.Data.SqlDbType.Int) { Value = id });
        }

        public List<Product> Search(string filter)
        {
            List<Product> result = new List<Product>();

            result = db.Products.Include("Category").Include("Supplier").
                Where(c => c.ProductName.Contains(filter) && 
                    !string.IsNullOrEmpty(filter)).ToList();

            return result;
        }
    }
}
