namespace TravelPics.Domains.Entities
{
    public class NotificationLog
    {
        public long Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public virtual NotificationStatus Status { get; set; }
        public string? Payload { get; set; }
        public virtual User Receiver { get; set; }
        public virtual ICollection<InAppNotification> InAppNotifications { get; set; }

        public NotificationLog()
        {
            InAppNotifications = new List<InAppNotification>();
        }

    }
}
