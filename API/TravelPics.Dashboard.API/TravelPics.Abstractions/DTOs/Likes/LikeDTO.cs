namespace TravelPics.Abstractions.DTOs.Likes
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTimeOffset LikedOn { get; set; }
        public DateTimeOffset? DislikedOn { get; set; }
    }
}
