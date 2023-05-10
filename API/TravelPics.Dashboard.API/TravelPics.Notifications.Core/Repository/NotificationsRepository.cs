using Microsoft.EntityFrameworkCore;
using TravelPics.Domains.DataAccess;
using TravelPics.Domains.Entities;
using TravelPics.Domains.Enums;

namespace TravelPics.Notifications.Core.Repository
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NotificationsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveInAppNotification(InAppNotification notification)
        {
            try
            {
                if (notification.NotificationLog == null) throw new Exception($"No Notification log found for new in app notification.");

                var notificationLog = await _dbContext.NotificationLogs.FirstOrDefaultAsync(u => u.Id == notification.NotificationLog.Id);

                if (notificationLog == null) throw new Exception($"No notificationLog found with id: {notification.NotificationLog.Id}");

                notification.NotificationLog = notificationLog;
                await _dbContext.InAppNotifications.AddAsync(notification);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not save new In App notification, reason: {ex.Message}");
            }

        }

        public async Task SaveNotificationLog(NotificationLog notificationLog)
        {
            try
            {
                if (notificationLog.Receiver == null) throw new Exception($"No Receiver found for new notification.");

                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == notificationLog.Receiver.Id);

                if (user == null) throw new Exception($"No user found with id: {notificationLog.Receiver.Id}");

                notificationLog.Receiver = user;
                await _dbContext.NotificationLogs.AddAsync(notificationLog);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not save new notification, reason: {ex.Message}");
            }
        }

        public async Task<NotificationStatus?> GetNotificationStatus(NotificationStatusEnum notificationStatusEnum)
        {
            return await _dbContext.NotificationStatuses.FirstOrDefaultAsync(n => n.Id == (int)notificationStatusEnum);
        }

        public async Task<NotificationType?> GetNotificationType(NotificationTypeEnum notificationTypeEnum)
        {
            return await _dbContext.NotificationTypes.FirstOrDefaultAsync(n => n.Id == (int)notificationTypeEnum);
        }

        public async Task<NotificationLog> GetNotificationLogById(long notificationLogId)
        {
            var notificationLog = await _dbContext.NotificationLogs
                .Include(n => n.Status)
                .Include(n => n.NotificationType)
                .Include(n => n.Receiver)
                .FirstOrDefaultAsync(n => n.Id == notificationLogId);

            if (notificationLog == null) throw new Exception($"Unable to find a notification log with id: {notificationLogId}");

            return notificationLog;
        }

        public async Task<IEnumerable<InAppNotification>> GetUserInAppNotifications(int userId)
        {
            var inAppNotifications = await _dbContext.InAppNotifications
                .Include(ian => ian.NotificationLog)
                .Where(ian => ian.NotificationLog.Receiver.Id == userId)
                .ToListAsync();

            return inAppNotifications;
        }
    }
}
