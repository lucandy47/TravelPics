using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelPics.Security.Models;

public class AuthorizationConfiguration
{
    [Required]
    public string SecurityKey { get; set; }
    public byte[] JwtSecurityKeyBytes => Encoding.ASCII.GetBytes(SecurityKey);
    public SymmetricSecurityKey JwtSecurityKey => new SymmetricSecurityKey(JwtSecurityKeyBytes);
    public SigningCredentials JwtSigningCredentials => new SigningCredentials(JwtSecurityKey, SecurityAlgorithms.HmacSha512Signature);
    [Required]
    public string Audience { get; set; }
    [Required]
    public string Issuer { get; set; }

}
