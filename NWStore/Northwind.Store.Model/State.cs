using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Store.Model
{
    /// <summary>
    /// Estado de la entidad.
    /// </summary>
    public enum State : short
    {
        Added,
        Unchanged,
        Modified,
        Deleted
    }
}
