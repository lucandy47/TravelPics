using TravelPics.Domains.Entities;
using TravelPics.Domains.Enums;

namespace TravelPics.Notifications.Core.Repository
{
    public interface INotificationsRepository
    {
        Task SaveInAppNotification(InAppNotification notification);
        Task<NotificationStatus?> GetNotificationStatus(NotificationStatusEnum notificationStatusEnum);
    }
}
