using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Domains.Enums;

namespace TravelPics.Notifications.Processors.Processors
{
    public interface INotificationProcessor
    {
        public NotificationTypeEnum NotificationTypeName { get; }
        public Task<bool> Run(NotificationLogDTO notificationLog);
    }
}
