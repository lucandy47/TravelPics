using Microsoft.EntityFrameworkCore;
using TravelPics.Domains.DataAccess;
using TravelPics.Domains.Entities;
using TravelPics.Users.Helpers;
using TravelPics.Users.Models;

namespace TravelPics.Users.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RegisterUser(UserCreate user)
    {
        var salt = PasswordHelper.GenerateSalt(128);
        var hash = PasswordHelper.GenerateHash(user.Password, salt, 10000, 64);
        await _dbContext.Users.AddAsync(new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            PasswordSalt = salt,
            PasswordHash = hash
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }


    public async Task<User?> GetUserById(int id)
    {
        var user = await _dbContext.Users
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }
}
