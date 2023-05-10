using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Domains.Entities;
using TravelPics.Domains.Enums;

namespace TravelPics.Abstractions.DTOs.Notifications
{
    public class InAppNotificationDTO
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset GeneratedOn { get; set; }
        public NotificationLogDTO NotificationLog { get; set; }
    }
}
