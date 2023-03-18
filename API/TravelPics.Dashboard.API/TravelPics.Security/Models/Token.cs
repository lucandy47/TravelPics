using System.ComponentModel.DataAnnotations;

namespace TravelPics.Security.Models;

public class Token
{
    [Required]
    public string AccessToken { get; set; }
    [Required]
    public DateTime ExpiresOn { get; set; }
}
