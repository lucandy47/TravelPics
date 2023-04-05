using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelPics.Documents.Abstraction;
using TravelPics.Documents.Abstraction.DTO;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;
        public DocumentsController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto( CancellationToken cancellationToken)
        {
            var filePath = "TestFiles/default.jpg";
            var content = System.IO.File.ReadAllBytes(filePath);

            var document = new DocumentDTO
            {
                Content = content,
                FileName = "default.jpg",
                UploadedById = 1,
                CreatedOn = DateTime.Now,
                Size = content.Length
            };

            try
            {
                await _documentsService.Save(document, new DocumentBlobContainerDTO()
                {
                    Id = 3
                }, "test", cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
