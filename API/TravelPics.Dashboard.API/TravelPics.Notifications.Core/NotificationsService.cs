using AutoMapper;
using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Domains.Entities;
using TravelPics.Notifications.Core.Repository;

namespace TravelPics.Notifications.Core
{
    public class NotificationsService : INotificationsService
    {
        private readonly IMapper _mapper;
        private readonly INotificationsRepository _notificationsRepository;

        public NotificationsService(IMapper mapper, INotificationsRepository notificationsRepository)
        {
            _mapper = mapper;
            _notificationsRepository = notificationsRepository;
        }

        public async Task<IEnumerable<InAppNotificationDTO>>? GetUserInAppNotifications(int userId)
        {
            var notifications = await _notificationsRepository.GetUserInAppNotifications(userId);

            var inAppNotificationsDTO = _mapper.Map<List<InAppNotificationDTO>>(notifications);

            return inAppNotificationsDTO;
        }

        public async Task SaveInAppNotification(InAppNotificationDTO notification)
        {
            var notificationLog = await _notificationsRepository.GetNotificationLogById(notification.NotificationLog.Id);

            if (notificationLog == null) throw new Exception($"Could not find the notification log attached to In App Notification");

            var inAppNotification = _mapper.Map<InAppNotification>(notification);

            if (inAppNotification == null) throw new Exception($"Unable to map In App Notification");
            inAppNotification.NotificationLog = notificationLog;

            await _notificationsRepository.SaveInAppNotification(inAppNotification);

        }

        public async Task SaveNotificationLog(NotificationLogDTO notificationLog)
        {
            var notificationStatus = await _notificationsRepository.GetNotificationStatus(notificationLog.Status);

            if (notificationStatus == null) throw new Exception($"No status found for current notification.");

            var notificationType = await _notificationsRepository.GetNotificationType(notificationLog.NotificationType);

            if (notificationType == null) throw new Exception($"No type found for current notification.");

            var notificationLogEntity = _mapper.Map<NotificationLog>(notificationLog);

            if (notificationLogEntity == null) throw new Exception($"Could not map Notification Log from DTO to entity.");

            notificationLogEntity.Status = notificationStatus;
            notificationLogEntity.NotificationType = notificationType;

            await _notificationsRepository.SaveNotificationLog(notificationLogEntity);
        }
    }
}
