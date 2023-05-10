using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Domains.Enums;

namespace TravelPics.Notifications.Processors.Processors
{
    public class InAppNotificationProcessor : INotificationProcessor
    {
        public NotificationTypeEnum NotificationTypeName => throw new NotImplementedException();

        public Task<bool> Run(NotificationLogDTO notificationLog)
        {
            throw new NotImplementedException();
        }
    }
}
