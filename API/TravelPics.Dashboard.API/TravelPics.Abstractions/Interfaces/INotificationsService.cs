using TravelPics.Abstractions.DTOs.Notifications;

namespace TravelPics.Abstractions.Interfaces
{
    public interface INotificationsService
    {
        Task SaveInAppNotification(InAppNotificationDTO notification);

    }
}
