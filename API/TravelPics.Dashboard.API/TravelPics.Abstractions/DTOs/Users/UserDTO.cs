using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.DTOs.Posts;

namespace TravelPics.Abstractions.DTOs.Users;

public class UserDTO: BasicUserInfoDTO
{
    public string Email { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public string? Phone { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public List<PostDTO>? Posts { get; set; }
    public DocumentDTO? ProfileImage { get; set; }
}
