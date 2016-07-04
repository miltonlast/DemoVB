using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    public interface IMaintenanceD<T, K>
    {
        T Read(K key);
        List<T> ReadList(PageFilter pf = null);
        void Delete(K key);
    }
}
