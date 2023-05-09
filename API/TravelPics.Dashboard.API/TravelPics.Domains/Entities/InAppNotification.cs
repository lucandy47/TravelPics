namespace TravelPics.Domains.Entities
{
    public class InAppNotification
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset GeneratedOn { get; set; }
        public virtual NotificationStatus Status {get; set;}
        public virtual User Receiver { get; set; }
    }
}
