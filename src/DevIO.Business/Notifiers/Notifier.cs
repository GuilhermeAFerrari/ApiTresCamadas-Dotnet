using DevIO.Business.Interfaces;

namespace DevIO.Business.Notifiers;

public class Notifier : INotifier
{
    private List<Notification> _notifications;

    public Notifier(List<Notification> notifications)
    {
        _notifications = new List<Notification>();
    }

    public void Handle(Notification notifier) => _notifications.Add(notifier);

    public List<Notification> GetNotifications() => _notifications;

    public bool HasNotification() => _notifications.Any();
}
