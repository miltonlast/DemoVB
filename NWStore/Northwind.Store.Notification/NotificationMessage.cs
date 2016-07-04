using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.Store.Notification
{
    public class NotificationMessage
    {
        public List<Message> Messages { get; set; }

        public NotificationMessage()
        {
            Messages = new List<Message>();
        }

        public bool IsNotified { get { return Messages.Count > 0; } }
    }
}
