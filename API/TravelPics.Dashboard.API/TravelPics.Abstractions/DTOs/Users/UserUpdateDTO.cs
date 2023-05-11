using TravelPics.Abstractions.DTOs.Documents;

namespace TravelPics.Abstractions.DTOs.Users
{
    public class UserUpdateDTO: BasicUserInfoDTO
    {
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DocumentDTO? ProfileImage { get; set; }
    }
}
