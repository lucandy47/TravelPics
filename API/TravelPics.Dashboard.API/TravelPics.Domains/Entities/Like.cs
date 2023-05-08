namespace TravelPics.Domains.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
        public DateTimeOffset LikedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DislikedOn { get; set; }
    }
}
