using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TravelPics.Documents.Abstraction.DTO;
using TravelPics.Locations.Abstraction.DTO;
using TravelPics.Posts.Abstraction;
using TravelPics.Posts.Abstraction.DTO;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
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
                        UploadedById = 1,
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
