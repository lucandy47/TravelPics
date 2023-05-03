using TravelPics.Abstractions.DTOs.Posts;

namespace TravelPics.Abstractions.DTOs.Users;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
    public string? Phone { get; set; }
    public List<PostDTO>? Posts { get; set; }
}
