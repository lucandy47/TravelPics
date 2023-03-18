using TravelPics.Users.Abstraction.DTO;

namespace TravelPics.Users.Abstraction;

public interface IUsersService
{
    Task<UserDTO> GetUserById(int id);
    Task<UserDTO> GetUserByEmail(string email);
    Task RegisterUser(UserCreateDTO userDTO);

}