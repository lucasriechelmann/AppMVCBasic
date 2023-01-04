using System.Collections.Generic;
using AppMVCBasic.Business.Notifications;

namespace AppMVCBasic.Business.Interfaces
{
    public interface INotifier
    {
        bool IsThereNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
