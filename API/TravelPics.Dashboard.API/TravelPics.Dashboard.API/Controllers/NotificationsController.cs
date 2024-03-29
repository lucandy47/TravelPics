﻿using Microsoft.AspNetCore.Mvc;
using TravelPics.Abstractions.Interfaces;
using TravelPics.Domains.Entities;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;
        private readonly IUsersService _usersService;

        public NotificationsController(INotificationsService notificationsService, IUsersService usersService)
        {
            _notificationsService = notificationsService;
            _usersService = usersService;
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetUserNotifications([FromRoute] int userId)
        {
            try
            {
                var notifications = await _notificationsService.GetUserInAppNotifications(userId);

                return Ok(notifications.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest($"Unable to retrieve notifications, reason:{ex.Message}");
            }
        }
        [HttpPut]
        [Route("read/{userId:int}")]
        public async Task<IActionResult> MarkAsRead([FromRoute] int userId)
        {
            try
            {
                await _notificationsService.MarkAsReadNotifications(userId);

                return Ok("Notifications have been marked as read.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Unable to mark notifications as read, reason:{ex.Message}");
            }
        }
    }
}
