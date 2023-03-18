using AutoMapper;
using TravelPics.Domains.Entities;
using TravelPics.Users.Abstraction.DTO;
using TravelPics.Users.Models;

namespace TravelPics.Users.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserDTO, User>();
        CreateMap<User, UserDTO>();

        CreateMap<UserCreateDTO, UserCreate>();
        CreateMap<UserCreate, UserCreateDTO>();
    }
}
