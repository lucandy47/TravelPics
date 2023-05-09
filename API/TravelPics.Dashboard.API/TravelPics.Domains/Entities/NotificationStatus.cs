namespace TravelPics.Domains.Entities
{
    public class NotificationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<InAppNotification> InAppNotifications { get; set; }

        public NotificationStatus()
        {
            InAppNotifications= new List<InAppNotification>();
        }
    }
}
