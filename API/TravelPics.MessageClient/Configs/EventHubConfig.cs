namespace TravelPics.Notifications.Configs
{
    public class EventHubConfig
    {
        public const string SectionName = "EventHub";
        public string? ConnectionString { get; set; }
        public string? Endpoint { get; set; }
        public string? NotificationsGroupId { get; set; }
        public string? NotificationsTopic { get; set; }
    }
}
