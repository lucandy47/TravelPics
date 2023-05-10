namespace TravelPics.Domains.Entities
{
    public class NotificationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }

        public NotificationType()
        {
            NotificationLogs = new List<NotificationLog>();
        }
    }
}
