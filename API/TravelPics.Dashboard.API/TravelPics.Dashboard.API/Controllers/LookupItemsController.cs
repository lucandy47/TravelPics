using Microsoft.AspNetCore.Mvc;
using TravelPics.Abstractions.Interfaces;

namespace TravelPics.Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupItemsController : ControllerBase
    {
        private readonly ILookupItemsService _lookupItemsService;
        public LookupItemsController(ILookupItemsService lookupItemsService)
        {
            _lookupItemsService = lookupItemsService;
        }

        [HttpGet("search-lookupitems")]
        public async Task<IActionResult> FindLookupItems([FromQuery] string searchKeyword)
        {

            if (Request.Query.Count > 1)
            {
                return BadRequest("Incorrect request - cannot have more than 1 parameter in query.");
            }

            return Ok(await _lookupItemsService.FindLookupItemsAsync(searchKeyword));
        }
    }
}
