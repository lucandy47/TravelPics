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
        public async Task<IActionResult> UploadPhoto(CancellationToken cancellationToken)
        {
            if (Request.Form.Files.Count == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            var file = Request.Form.Files[0];

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            var content = memoryStream.ToArray();

            var document = new DocumentDTO
            {
                Content = content,
                FileName = file.FileName,
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
