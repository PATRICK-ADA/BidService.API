using BidService.API.Abstraction;
using BidService.API.BidService.Domain.RequestDto;
using Microsoft.AspNetCore.Mvc;

namespace BidService.API.BidService.Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {

        private readonly IBidService _bidService;

        public BidsController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetBidEntriesById([FromQuery] Guid Id)
        {
            var result = await _bidService.GetBidEntriesById(Id);
            return result is null ? BadRequest(result) : Ok(result);
        }

        [HttpGet("UserName")]
        public async Task<IActionResult> GetBidEntriesByUserName([FromQuery] string UserName)
        {

            var result = await _bidService.GetBidEntriesByUserName(UserName);
            return result is null ? BadRequest(result) : Ok(result);


        }

        [HttpGet("All")]
        public IActionResult GetAllBidEntries([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = _bidService.GetAllBidEntries(pageNumber, pageSize);


            return result is null ? BadRequest(result) : Ok(result);

        }
    }
}
