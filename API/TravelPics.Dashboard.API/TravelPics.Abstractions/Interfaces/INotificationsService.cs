using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Domains.Entities;
using TravelPics.Domains.Enums;

namespace TravelPics.Abstractions.Interfaces
{
    public interface INotificationsService
    {
        Task<long> SaveNotificationLog(NotificationLogDTO notificationLog);
        Task SaveInAppNotification(InAppNotificationDTO notification, long notificationLogId);
        Task<IEnumerable<InAppNotificationDTO>>? GetUserInAppNotifications(int userId);
        Task UpdateNotificationStatus(long notificationLogId, NotificationStatusEnum notificationStatusEnum);
        Task MarkAsReadNotifications(int userId);
    }
}
