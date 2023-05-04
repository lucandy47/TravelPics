using TravelPics.Abstractions.DTOs.Documents;

namespace TravelPics.Abstractions.DTOs.Users
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public DocumentDTO? ProfileImage { get; set; }
    }
}
