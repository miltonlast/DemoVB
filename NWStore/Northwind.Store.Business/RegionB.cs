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
    public class RegionB : IDisposable, IMaintenanceB<Region, int>
    {
        RegionD rD = new RegionD();

        public void Save(Region r, NotificationMessage nm = null)
        {
            rD.Save(r, nm);
        }

        public Region Read(int id)
        {
            return rD.Read(id);
        }

        public Region Read(string description)
        {
            return rD.Read(description);
        }

        public List<Region> ReadList(PageFilter pf = null)
        {
            return rD.ReadList(pf);
        }

        public void Delete(int id)
        {
            rD.Delete(id);
        }

        public void Create(Region model, NotificationMessage nm = null)
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

                if (Read(model.RegionDescription) != null)
                {
                    Message m = new Message()
                    {
                        Title = Store.Resources.ApplicationMessages.RegionExist
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

        public void Dispose()
        {
            rD.Dispose();
        }
    }
}
