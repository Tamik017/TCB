using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Model
{
    public class Notifi
    {
        public Notifi() {  }
        public void ShowNotification(string Message, NotificationType Type)
        {
            var notificationManager = new NotificationManager();
            notificationManager.Show("TwitchBot", Message, Type);
        }
    }
}
