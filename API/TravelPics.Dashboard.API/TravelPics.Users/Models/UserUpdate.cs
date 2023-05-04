using TravelPics.Domains.Entities;

namespace TravelPics.Users.Models
{
    public class UserUpdate: UserBasic
    {
        public int Id { get; set; }
        public Document? ProfileImage { get; set; }
    }
}
