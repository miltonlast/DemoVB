using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.Notification;

namespace Northwind.Store.Business
{
    public class ProductB : IDisposable, IMaintenanceB<Product, int>
    {
        ProductD pD = new ProductD();

        public void Save(Product p, NotificationMessage nm = null)
        {
            pD.Save(p, nm);
        }

        public Product Read(int id)
        {
            return pD.Read(id);
        }

        public Product Read(string description)
        {
            return pD.Read(description);
        }

        public List<Product> ReadList(PageFilter pf = null)
        {
            return pD.ReadList(pf);
        }

        public void Delete(int id)
        {
            pD.Delete(id);
        }

        public void Create(Product model, NotificationMessage nm = null)
        {
            // Validaciones
            if (nm != null)
            {
                if (model.State != State.Added)
                {
                    Message m = new Message()
                    {
                        Title = Store.Resources.GeneralMessages.DataRequiredStateAdded
                    };
                    nm.Messages.Add(m);
                }

                if (Read(model.ProductName) != null)
                {
                    Message m = new Message()
                    {
                        Title = Store.Resources.ApplicationMessages.ProductExist
                    };
                    nm.Messages.Add(m);
                }

                if (!nm.IsNotified)
                {
                    Save(model, nm);
                }
            }
            else
            {
                Save(model, nm);
            }
        }

        public List<Product> Search(string filter)
        {
            return pD.Search(filter);
        }

        public void Dispose()
        {
            pD.Dispose();
        }
    }
}
