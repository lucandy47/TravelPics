using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.DTOs.Locations;
using TravelPics.Abstractions.DTOs.Users;

namespace TravelPics.Abstractions.DTOs.Posts
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int CreatedById { get; set; }
        public List<DocumentDTO> Photos { get; set; }
        public LocationDTO Location { get; set; }
        public DateTimeOffset PublishedOn { get; set; }
        public UserPostInfoDTO User { get; set; }
    }
}
