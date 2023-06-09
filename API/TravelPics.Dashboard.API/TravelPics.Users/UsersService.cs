using AutoMapper;
using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Domains.Entities;
using TravelPics.Users.Models;
using TravelPics.Users.Repository;

namespace TravelPics.Users;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IDocumentsService _documentsService;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper, IDocumentsService documentsService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _documentsService = documentsService;
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

    public async Task<int> UpdateUser(UserUpdateDTO user)
    {
        var userEntity = _mapper.Map<UserUpdate>(user);

        if (userEntity == null) throw new Exception($"Could not map the updated user.");

        if(user.ProfileImage != null)
        {
            CancellationToken cancellationToken = new CancellationToken();

            var documentBlobContainerDTO = new DocumentBlobContainerDTO()
            {
                Id = 4
            };
            var userProfileImagePath = $"{user.Id}_{user.Email}_{user.FirstName}-{user.LastName}";

            var photo = await _documentsService.ComputeDocument(user.ProfileImage, documentBlobContainerDTO, userProfileImagePath, true, cancellationToken);

            userEntity.ProfileImage = photo;

            try
            {
                await _documentsService.UploadPhotos(new List<Document>()
            {
                photo
            }, documentBlobContainerDTO, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to upload profile picture to cloud.", ex);
            }
        }

        await _usersRepository.UpdateUser(userEntity);

        return user.Id;
    }
}