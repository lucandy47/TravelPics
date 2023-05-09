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
        public async Task SaveInAppNotification(InAppNotificationDTO notification)
        {
            var notificationStatus = await _notificationsRepository.GetNotificationStatus(notification.Status);

            if (notificationStatus == null) throw new Exception($"No status found for current notification.");

            var notificationEntity = _mapper.Map<InAppNotification>(notification);

            if (notificationEntity == null) throw new Exception($"Could not map In App Notification from DTO to entity.");

            notificationEntity.Status = notificationStatus;

            await _notificationsRepository.SaveInAppNotification(notificationEntity);

        }
    }
}
