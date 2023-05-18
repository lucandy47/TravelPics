using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Domains.Entities;
using TravelPics.Domains.Enums;
using TravelPics.Notifications.Core.Repository;

namespace TravelPics.Notifications.Core
{
    public class NotificationsService : INotificationsService
    {
        private readonly IMapper _mapper;
        private readonly INotificationsRepository _notificationsRepository;
        private readonly IUsersService _usersService;

        public NotificationsService(IMapper mapper, INotificationsRepository notificationsRepository, IUsersService usersService)
        {
            _mapper = mapper;
            _notificationsRepository = notificationsRepository;
            _usersService = usersService;
        }

        public async Task<IEnumerable<InAppNotificationDTO>>? GetUserInAppNotifications(int userId)
        {
            var notifications = await _notificationsRepository.GetUserInAppNotifications(userId);

            var inAppNotificationsDTO = _mapper.Map<List<InAppNotificationDTO>>(notifications.ToList());

            foreach (var inAppNotif in inAppNotificationsDTO)
            {
                var notif = notifications.FirstOrDefault(n => n.Id == inAppNotif.Id);

                if (notif == null || !notif.NotificationLog.SenderId.HasValue) continue;

                var sender = await _usersService.GetUserById(notif.NotificationLog.SenderId.Value);

                if (sender == null) continue;

                inAppNotif.NotificationLog.Sender = new Abstractions.DTOs.Users.UserPostInfoDTO()
                {
                    FirstName = sender.FirstName,
                    LastName = sender.LastName,
                    ProfileImage = sender.ProfileImage,
                    Id = sender.Id,
                    CreatedOn = sender.CreatedOn,
                };
            }

            return inAppNotificationsDTO;
        }

        public async Task MarkAsReadNotifications(int userId)
        {
            await _notificationsRepository.MarkAsReadNotifications(userId);
        }

        public async Task SaveInAppNotification(InAppNotificationDTO notification, long notificationLogId)
        {
            var notificationLog = await _notificationsRepository.GetNotificationLogById(notificationLogId);

            if (notificationLog == null) throw new Exception($"Could not find the notification log attached to In App Notification");

            var inAppNotification = _mapper.Map<InAppNotification>(notification);

            if (inAppNotification == null) throw new Exception($"Unable to map In App Notification");
            inAppNotification.NotificationLog = notificationLog;

            await _notificationsRepository.SaveInAppNotification(inAppNotification, notificationLogId);

        }

        public async Task<long> SaveNotificationLog(NotificationLogDTO notificationLog)
        {
            var notificationStatus = await _notificationsRepository.GetNotificationStatus(notificationLog.Status);

            if (notificationStatus == null) throw new Exception($"No status found for current notification.");

            var notificationType = await _notificationsRepository.GetNotificationType(notificationLog.NotificationType);

            if (notificationType == null) throw new Exception($"No type found for current notification.");

            var notificationLogEntity = _mapper.Map<NotificationLog>(notificationLog);

            if (notificationLogEntity == null) throw new Exception($"Could not map Notification Log from DTO to entity.");

            notificationLogEntity.Status = notificationStatus;
            notificationLogEntity.NotificationType = notificationType;

            var notificationLogId = await _notificationsRepository.SaveNotificationLog(notificationLogEntity);

            return notificationLogId;
        }

        public async Task UpdateNotificationStatus(long notificationLogId, NotificationStatusEnum notificationStatusEnum)
        {
            await _notificationsRepository.UpdateNotificationStatus(notificationLogId, notificationStatusEnum);
        }
    }
}
