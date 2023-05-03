using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Security.Models;

namespace TravelPics.Security;

public class AuthenticationService: IAuthenticationService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly IUsersService _usersService;
    private readonly AuthorizationConfiguration _authorizationConfiguration;
    public AuthenticationService(IUsersService usersService, AuthorizationConfiguration authorizationConfiguration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        _usersService = usersService;
        _authorizationConfiguration = authorizationConfiguration;
    }

    private Token CreateAuthorizationToken(UserDTO user)
    {
        var expiresOn = DateTime.UtcNow.AddHours(6);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            IssuedAt = DateTime.Now,
            Expires = expiresOn,
            SigningCredentials = _authorizationConfiguration.JwtSigningCredentials,
            Issuer = _authorizationConfiguration.Issuer,
            Audience = _authorizationConfiguration.Audience,
            Claims = new Dictionary<string, object>()
                {
                    {TravelPicsClaimTypes.UserId, user.Id.ToString()},
                    {ClaimTypes.Email, user.Email},
                    {TravelPicsClaimTypes.FullName, user.FirstName+", "+user.LastName},
                    {ClaimTypes.NameIdentifier, user.Email},
                }
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return new Token
        {
            AccessToken = _tokenHandler.WriteToken(token),
            ExpiresOn = expiresOn
        };
    }

    public async Task<Token?> AuthenticateAsync(LoginModel loginModel)
    {
        var user = await _usersService.GetUserByEmail(loginModel.Email);
        if (user == null) return null;

        byte[] hash = Convert.FromBase64String(user.PasswordHash);
        byte[] salt = Convert.FromBase64String(user.PasswordSalt);

        // Verify the user's password
        if (!VerifyPasswordHash(loginModel.Password, hash, salt))
            return null;

        var token = CreateAuthorizationToken(user);
        return token;
    }
    private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        if (password == null) throw new ArgumentNullException("password");
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
        if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
        if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

        using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }
        }
        return true;
    }

}