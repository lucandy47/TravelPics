using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.DTOs.Locations;
using TravelPics.Abstractions.DTOs.Posts;
using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Posts;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            return Ok(await _usersService.GetUserById(userId));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO userCreateDTO)
        {
            if (userCreateDTO == null || string.IsNullOrWhiteSpace(userCreateDTO.Password) || string.IsNullOrWhiteSpace(userCreateDTO.Email))
            {
                return BadRequest("Could not register new user!");
            }

            if (!string.IsNullOrEmpty(userCreateDTO.FirstName) && !Regex.IsMatch(userCreateDTO.FirstName, @"^[A-Za-z]+$"))
            {
                return BadRequest("First name can only contain letters.");
            }

            if (!string.IsNullOrEmpty(userCreateDTO.LastName) && !Regex.IsMatch(userCreateDTO.LastName, @"^[A-Za-z]+$"))
            {
                return BadRequest("Last name can only contain letters.");
            }

            if (!string.IsNullOrEmpty(userCreateDTO.Phone))
            {
                if (!Regex.IsMatch(userCreateDTO.Phone, @"^[0-9+]+$"))
                {
                    return BadRequest("Invalid phone number format.");
                }

                if (userCreateDTO.Phone.Length < 7 || userCreateDTO.Phone.Length > 15)
                {
                    return BadRequest("Phone number length should be between 7 and 15 characters.");
                }
            }

            try
            {
                await _usersService.RegisterUser(userCreateDTO);
                return Ok(new { message = "User successfully registered!" });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(CancellationToken cancellationToken)
        {
            var userUpdateDTO = await ComputePostFromRequestForm(Request.Form);

            if (userUpdateDTO == null)
            {
                return BadRequest("Could not update user.");
            }
            if (!string.IsNullOrEmpty(userUpdateDTO.FirstName) && !Regex.IsMatch(userUpdateDTO.FirstName, @"^[A-Za-z]+$"))
            {
                return BadRequest("First name can only contain letters.");
            }

            if (!string.IsNullOrEmpty(userUpdateDTO.LastName) && !Regex.IsMatch(userUpdateDTO.LastName, @"^[A-Za-z]+$"))
            {
                return BadRequest("Last name can only contain letters.");
            }

            if (!string.IsNullOrEmpty(userUpdateDTO.Phone))
            {
                if (!Regex.IsMatch(userUpdateDTO.Phone, @"^[0-9+]+$"))
                {
                    return BadRequest("Invalid phone number format.");
                }

                if (userUpdateDTO.Phone.Length < 7 || userUpdateDTO.Phone.Length > 15)
                {
                    return BadRequest("Phone number length should be between 7 and 15 characters.");
                }
            }

            try
            {
                await _usersService.UpdateUser(userUpdateDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<UserUpdateDTO> ComputePostFromRequestForm(IFormCollection formCollection)
        {
            var formFields = formCollection.Where(x => x.Key != "ProfileImage").ToDictionary(x => x.Key, x => x.Value.ToString());
            var profileImage = new DocumentDTO();

            DocumentDTO? document = null;

            if (Request.Form.Files.Any())
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();

                    document = new DocumentDTO
                    {
                        Content = content,
                        FileName = file.FileName,
                        UploadedById = int.Parse(formFields["Id"]),
                        CreatedOn = DateTime.Now,
                        Size = content.Length
                    };
                }
            }
            

            return new UserUpdateDTO
            {
                Id = int.Parse(formFields["Id"]),
                FirstName = formFields["FirstName"],
                LastName = formFields["LastName"],
                Email = formFields["Email"],
                Phone = formFields["Phone"],
                ProfileImage = document,
            };
        }

    }
}
