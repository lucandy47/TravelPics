using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            if(userCreateDTO == null || string.IsNullOrWhiteSpace(userCreateDTO.Password) || string.IsNullOrWhiteSpace(userCreateDTO.Email))
            {
                return BadRequest();
            }
            await _usersService.RegisterUser(userCreateDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(CancellationToken cancellationToken)
        {
            var userUpdateDTO = await ComputePostFromRequestForm(Request.Form);

            if (userUpdateDTO == null)
            {
                return BadRequest("Could not update user.");
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

            var file = Request.Form.Files[0];
            DocumentDTO? document = null;

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

            return new UserUpdateDTO
            {
                Id = int.Parse(formFields["Id"]),
                FirstName = formFields["FirstName"],
                LastName = formFields["LastName"],
                Phone = formFields["Phone"],
                ProfileImage = document,
            };
        }
    }
}
