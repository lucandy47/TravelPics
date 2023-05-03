using TravelPics.Abstractions.DTOs.Users;

namespace TravelPics.Abstractions.Interfaces
{
    public interface IUsersService
    {
        Task<UserDTO?> GetUserById(int id);
        Task<UserDTO?> GetUserByEmail(string email);
        Task RegisterUser(UserCreateDTO user);
    }
}
