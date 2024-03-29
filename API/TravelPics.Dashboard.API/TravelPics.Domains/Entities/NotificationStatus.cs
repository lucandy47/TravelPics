﻿namespace TravelPics.Domains.Entities
{
    public class NotificationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }

        public NotificationStatus()
        {
            NotificationLogs = new List<NotificationLog>();
        }
    }
}
