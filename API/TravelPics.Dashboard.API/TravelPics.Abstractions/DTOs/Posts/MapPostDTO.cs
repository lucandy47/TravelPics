using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.DTOs.Locations;
using TravelPics.Abstractions.DTOs.Users;

namespace TravelPics.Abstractions.DTOs.Posts
{
    public class MapPostDTO
    {
        public int Id { get; set; }
        public List<DocumentDTO> Photos { get; set; }
        public LocationDTO Location { get; set; }
        public DateTimeOffset PublishedOn { get; set; }
        public UserPostInfoDTO User { get; set; }
        public MapPostDTO()
        {
            Photos = new List<DocumentDTO>();
        }
    }
}
