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
                if (notification.Receiver == null) throw new Exception($"No Receiver found for new notification.");

                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == notification.Receiver.Id);

                if (user == null) throw new Exception($"No user found with id: {notification.Receiver.Id}");

                notification.Receiver = user;
                await _dbContext.InAppNotifications.AddAsync(notification);
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

    }
}
