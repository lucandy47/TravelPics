﻿namespace TravelPics.Domains.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string? Phone { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Document? ProfileImage { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<InAppNotification> InAppNotifications { get; set; }
        public User()
        {
            Posts = new List<Post>();
            Likes = new List<Like>();
            InAppNotifications = new List<InAppNotification>();
        }
    }
}
