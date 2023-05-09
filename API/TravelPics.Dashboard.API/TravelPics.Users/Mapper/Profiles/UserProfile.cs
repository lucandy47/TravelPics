using AutoMapper;
using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Domains.Entities;
using TravelPics.Users.Models;

namespace TravelPics.Users.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserDTO, User>();
        CreateMap<User, UserDTO>();
        CreateMap<User, UserPostInfoDTO>();
        CreateMap<UserPostInfoDTO, User>();

        CreateMap<UserCreateDTO, UserCreate>();
        CreateMap<UserCreate, UserCreateDTO>();

        CreateMap<UserUpdateDTO, UserUpdate>();
        CreateMap<UserUpdate, UserUpdateDTO>();
    }
}
