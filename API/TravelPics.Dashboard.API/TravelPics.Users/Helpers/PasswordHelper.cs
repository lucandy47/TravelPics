using System.Security.Cryptography;
using System.Text;

namespace TravelPics.Users.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt(int length)
        {
            var salt = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string password, string salt, int iterations, int hashLength)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var hmac = new HMACSHA512(saltBytes))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
