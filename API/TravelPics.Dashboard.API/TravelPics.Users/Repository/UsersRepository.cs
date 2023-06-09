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

        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (existingUser != null) throw new Exception($"An user with this email address: '{user.Email}' already exists!");

        await _dbContext.Users.AddAsync(new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            PasswordSalt = salt,
            PasswordHash = hash,
            CreatedOn = DateTimeOffset.UtcNow,
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
                .ThenInclude(p => p.Location)
            .Include(u => u.ProfileImage)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }

    public async Task<int> UpdateUser(UserUpdate user)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

        if (userEntity != null)
        {
            if(user.ProfileImage != null)
            {
                userEntity.ProfileImage = user.ProfileImage;
            }
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Phone = user.Phone;

            await _dbContext.SaveChangesAsync();
        }

        return user.Id;
    }
}
