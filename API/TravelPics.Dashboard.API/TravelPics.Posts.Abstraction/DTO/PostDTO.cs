using TravelPics.Documents.Abstraction.DTO;
using TravelPics.Locations.Abstraction.DTO;

namespace TravelPics.Posts.Abstraction.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int CreatedById { get; set; }
        public List<DocumentDTO> Photos { get; set; }
        public LocationDTO Location { get; set; }
        public DateTimeOffset PublishedOn { get; set; }

    }
}
