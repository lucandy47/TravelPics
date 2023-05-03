using Microsoft.AspNetCore.Mvc;
using TravelPics.Abstractions.DTOs.Users;
using TravelPics.Abstractions.Interfaces;

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
    }
}
