using Microsoft.AspNetCore.Mvc;
using TravelPics.Abstractions.Interfaces;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;
        public NotificationsController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
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
