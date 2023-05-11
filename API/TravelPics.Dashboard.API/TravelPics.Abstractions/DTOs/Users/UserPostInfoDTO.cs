using TravelPics.Abstractions.DTOs.Documents;

namespace TravelPics.Abstractions.DTOs.Users
{
    public class UserPostInfoDTO: BasicUserInfoDTO
    {
        public DateTimeOffset CreatedOn { get; set; }
        public DocumentDTO? ProfileImage { get; set; }
    }
}
