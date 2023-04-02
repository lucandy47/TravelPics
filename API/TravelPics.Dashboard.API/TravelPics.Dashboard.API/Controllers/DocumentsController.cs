using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelPics.Documents.Abstraction;

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
    }
}
