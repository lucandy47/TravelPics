using TravelPics.Domains.Entities;
using TravelPics.Domains.Enums;

namespace TravelPics.Notifications.Core.Repository
{
    public interface INotificationsRepository
    {
        Task SaveNotificationLog(NotificationLog notificationLog);
        Task SaveInAppNotification(InAppNotification notification);
        Task<NotificationStatus?> GetNotificationStatus(NotificationStatusEnum notificationStatusEnum);
        Task<NotificationType?> GetNotificationType(NotificationTypeEnum notificationTypeEnum);
        Task<NotificationLog> GetNotificationLogById(long notificationLogId);
        Task<IEnumerable<InAppNotification>> GetUserInAppNotifications(int userId);
    }
}
