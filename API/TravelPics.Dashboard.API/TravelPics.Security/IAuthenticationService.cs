using TravelPics.Security.Models;

namespace TravelPics.Security
{
    public interface IAuthenticationService
    {
        Task<Token?> AuthenticateAsync(LoginModel loginModel);
    }
}
