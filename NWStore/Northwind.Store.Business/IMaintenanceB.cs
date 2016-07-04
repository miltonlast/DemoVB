using Northwind.Store.Data;
using Northwind.Store.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.Business
{
    public interface IMaintenanceB<T, K>
    {
        void Save(T c, NotificationMessage nm = null);
        T Read(K key);
        List<T> ReadList(PageFilter pf = null);
        void Delete(K key);
    }
}
