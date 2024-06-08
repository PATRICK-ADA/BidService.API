using BidService.API.BidService.Core.ApiResponse;
using BidService.API.BidService.Core.Utility.Common;
using BidService.API.BidService.Domain.Entities;
using BidService.API.BidService.Domain.RequestDto;


namespace BidService.API.Abstraction
{
    public interface IBidService
    {
        Task<ApiResponse<object>> GetBidEntriesById(Guid Id);
        Task<ApiResponse<object>> GetBidEntriesByUserName(string UserName);
        PagedResponse<BidDto> GetAllBidEntries(int pageNumber, int pageSize);


    }
}
