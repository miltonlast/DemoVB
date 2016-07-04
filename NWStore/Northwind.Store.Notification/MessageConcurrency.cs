using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Store.Notification
{
    public class MessageConcurrency : Message
    {
        public object Original { get; set; }
        public object Current { get; set; }
    }
}
