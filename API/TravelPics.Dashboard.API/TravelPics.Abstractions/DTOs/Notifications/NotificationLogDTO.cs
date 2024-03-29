﻿using TravelPics.Abstractions.DTOs.Posts;
using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Domains.Enums;

namespace TravelPics.Abstractions.DTOs.Notifications
{
    public class NotificationLogDTO
    {
        public long Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
        public NotificationStatusEnum Status { get; set; }
        public UserPostInfoDTO Receiver { get; set; }
        public UserPostInfoDTO Sender { get; set; }
        public string? Payload { get; set; }
        public string? PostDescription { get; set; }
        public int PostId { get; set; }
    }
}
