using TravelPics.Abstractions.DTOs.Documents;

namespace TravelPics.Abstractions.DTOs.Users
{
    public class UserPostInfoDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DocumentDTO? ProfileImage { get; set; }
    }
}
