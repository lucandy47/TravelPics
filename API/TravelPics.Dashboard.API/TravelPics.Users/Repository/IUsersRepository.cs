using TravelPics.Domains.Entities;
using TravelPics.Users.Models;

namespace TravelPics.Users.Repository;

public interface IUsersRepository
{
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByEmail(string email);
    Task RegisterUser(UserCreate user);
}
