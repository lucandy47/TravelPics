﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelPics.Security;
using TravelPics.Security.Models;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var token = await _authenticationService.AuthenticateAsync(loginModel);
            if (token == null)
            {
                return BadRequest(new { message = "Invalid email or password" });
            }

            return Ok(token);
        }
    }
}
