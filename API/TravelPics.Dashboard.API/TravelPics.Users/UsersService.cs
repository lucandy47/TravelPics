using AutoMapper;
using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Users.Models;
using TravelPics.Users.Repository;

namespace TravelPics.Users;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<UserDTO> GetUserByEmail(string email)
    {
        var user = await _usersRepository.GetUserByEmail(email);

        var userDTO = _mapper.Map<UserDTO>(user);

        return userDTO;
    }

    public async Task<UserDTO> GetUserById(int id)
    {
        var user = await _usersRepository.GetUserById(id);

        var userDTO =  _mapper.Map<UserDTO>(user);

        userDTO.PasswordHash = string.Empty;
        userDTO.PasswordSalt = string.Empty;

        return userDTO;
    }

    public async Task RegisterUser(UserCreateDTO userDTO)
    {
        var user = _mapper.Map<UserCreate>(userDTO);
        await _usersRepository.RegisterUser(user);
    }
}