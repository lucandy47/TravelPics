using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TravelPics.Abstractions.DTOs.Documents;
using TravelPics.Abstractions.DTOs.Likes;
using TravelPics.Abstractions.DTOs.Locations;
using TravelPics.Abstractions.DTOs.Notifications;
using TravelPics.Abstractions.DTOs.Posts;
using TravelPics.Abstractions.Interfaces;
using TravelPics.MessageClient;
using TravelPics.Notifications.Configs;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly INotificationsService _notificationsService;
        private readonly EventHubConfig _eventHubConfig;
        private readonly IProducer _producer;
        private readonly IUsersService _usersService;


        public PostsController(IPostsService postsService, INotificationsService notificationsService,
            IOptions<EventHubConfig> eventHubConfigOptions,
            IProducer producer,
            IUsersService usersService)
        {
            _postsService = postsService;
            _notificationsService = notificationsService;
            _eventHubConfig = eventHubConfigOptions.Value;
            _producer = producer;
            _usersService = usersService;
        }

        [HttpPost]
        [Route("like")]
        public async Task<IActionResult> LikePost([FromBody] LikeModel like)
        {
            if (string.IsNullOrWhiteSpace(_eventHubConfig.NotificationsTopic)) return BadRequest($"The topic cannot be null.");

            try
            {
                await _postsService.LikePost(like);
                var notification = await CreateNotification(like.PostId,like.UserId);
                try
                {
                    var notificationLogId = await _notificationsService.SaveNotificationLog(notification);
                    notification.Id = notificationLogId;

                    if(notificationLogId > 0)
                    {
                        await _producer.ProduceMessage<NotificationLogDTO>(_eventHubConfig.NotificationsTopic, notification);
                    }
                }
                catch(Exception ex)
                {
                    return BadRequest($"Could not create notification, reason: {ex.Message}");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not like post, reason: {ex.Message}");
            }
        }

        private async Task<NotificationLogDTO> CreateNotification(int postId, int userId)
        {
            var postDto = await _postsService.GetPostById(postId);

            var sender = await _usersService.GetUserById(userId);

            postDto.User.ProfileImage = null;

            var notification = new NotificationLogDTO()
            {
                CreatedOn = DateTimeOffset.Now,
                Status = Domains.Enums.NotificationStatusEnum.Created,
                Payload = "Like",
                NotificationType = Domains.Enums.NotificationTypeEnum.InAppNotification,
                Receiver = postDto.User,
                Sender = new Abstractions.DTOs.Users.BasicUserInfoDTO()
                {
                    FirstName = sender.FirstName,
                    LastName = sender.LastName,
                    Id = userId
                }
            };

            return notification;
        }

        [HttpPost]
        [Route("dislike")]
        public async Task<IActionResult> DislikePost([FromBody] LikeModel like)
        {
            try
            {
                await _postsService.DislikePost(like);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestPosts()
        {
            try
            {
                var posts = await _postsService.GetLatestPosts();

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("user/{userId:int}")]
        public async Task<IActionResult> GetUserPosts([FromRoute] int userId)
        {
            try
            {
                var posts = await _postsService.GetUserPosts(userId);

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(CancellationToken cancellationToken)
        {
            var postDTO = await ComputePostFromRequestForm(Request.Form);

            if (postDTO == null)
            {
                return BadRequest("Could not create post.");
            }

            if (!postDTO.Photos.Any())
            {
                return BadRequest("No photos attached to the post.");
            }

            if(postDTO.Location == null)
            {
                return BadRequest("No location attached to the post.");
            }

            try
            {
                await _postsService.SavePost(postDTO, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        private async Task<PostDTO> ComputePostFromRequestForm(IFormCollection formCollection)
        {
            var formFields = formCollection.Where(x => x.Key != "Photos").ToDictionary(x => x.Key, x => x.Value.ToString());
            var photoFiles = new List<DocumentDTO>();

            foreach (var file in Request.Form.Files)
            {
                if (file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();

                    var document = new DocumentDTO
                    {
                        Content = content,
                        FileName = file.FileName,
                        UploadedById = int.Parse(formFields["CreatedById"]),
                        CreatedOn = DateTime.Now,
                        Size = content.Length
                    };
                    photoFiles.Add(document);
                }
            }

            return new PostDTO
            {
                Description = formFields["Description"],
                Location = JsonConvert.DeserializeObject<LocationDTO>(formFields["Location"]) ?? new LocationDTO(),
                CreatedById = int.Parse(formFields["CreatedById"]),
                Photos = photoFiles,
                PublishedOn = DateTime.Now,
            };
        }
    }
}
