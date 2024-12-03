using Flunt.Notifications;

namespace Desafio.Infrastructure
{
    public interface INotificationContext
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }

        void AddNotification(IReadOnlyCollection<Notification> errors);
        void AddNotification(string key, string message);
    }
}
