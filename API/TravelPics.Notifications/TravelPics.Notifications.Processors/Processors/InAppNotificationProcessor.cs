using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Domains.Enums;
using TravelPics.Notifications.Processors.Enums;

namespace TravelPics.Notifications.Processors.Processors
{
    public class InAppNotificationProcessor : INotificationProcessor
    {
        public NotificationTypeEnum NotificationTypeName => NotificationTypeEnum.InAppNotification;
        private readonly INotificationsService _notificationsService;

        public InAppNotificationProcessor(
            INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }
        public async Task<bool> Run(NotificationLogDTO notificationLog)
        {
            if (string.IsNullOrWhiteSpace(notificationLog.Payload)) throw new ArgumentNullException($"Payload cannot be null");

            if (notificationLog.Receiver == null) throw new Exception($"Receiver of the in app notification cannot be null.");

            if (notificationLog.Sender == null) throw new Exception($"Sender of the in app notification cannot be null.");

            var subject = GetSubject(notificationLog);

            var inAppNotification = new InAppNotificationDTO()
            {
                GeneratedOn = DateTimeOffset.Now,
                Subject = subject
            };

            await _notificationsService.SaveInAppNotification(inAppNotification, notificationLog.Id);
            return true;
        }

        private static string GetSubject(NotificationLogDTO notificationLog)
        {
            var subject = string.Empty;

            switch (notificationLog.Payload)
            {
                case NotificationPayloadType.Like:
                    if (!string.IsNullOrWhiteSpace(notificationLog.PostDescription))
                    {
                        subject = $"{notificationLog.Sender.FirstName} {notificationLog.Sender.LastName} liked your post - \"{notificationLog.PostDescription}\".";
                    }
                    else
                    {
                        subject = $"{notificationLog.Sender.FirstName} {notificationLog.Sender.LastName} liked your post.";
                    }
                    break;
                case NotificationPayloadType.Comment:
                    subject = $"{notificationLog.Sender.FirstName} {notificationLog.Sender.LastName} commented your post - \"{notificationLog.PostDescription}\".";
                    break;
                default:
                    break;
            }
            return subject;
        }
    }
}
