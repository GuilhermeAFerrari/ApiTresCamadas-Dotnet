using DevIO.Business.Notifiers;

namespace DevIO.Business.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notifier);
}
