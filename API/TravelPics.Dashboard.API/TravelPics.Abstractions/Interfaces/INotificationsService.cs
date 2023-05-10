using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Domains.Entities;

namespace TravelPics.Abstractions.Interfaces
{
    public interface INotificationsService
    {
        Task SaveNotificationLog(NotificationLogDTO notificationLog);
        Task SaveInAppNotification(InAppNotificationDTO notification);
        Task<IEnumerable<InAppNotificationDTO>>? GetUserInAppNotifications(int userId);

    }
}
