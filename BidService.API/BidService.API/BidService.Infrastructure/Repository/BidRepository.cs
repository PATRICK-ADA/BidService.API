using BidService.API.BidService.Core.ApiResponse;
using BidService.API.BidService.Core.Utility.Common;
using BidService.API.BidService.Domain.Entities;
using BidService.API.BidService.Domain.RequestDto;
using BidService.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using RoomService.Infrastructure.Data;



namespace BidService.API.Repository
{
    public class BidRepository : IBidRepository
    {
        private readonly AppDbContext _Context;

        public BidRepository(AppDbContext Context)
        {
            _Context = Context;

        }
       

        public async Task<ApiResponse<object>> GetBidEntriesById(Guid Id)
        {
            if (Id.ToString() is null)
            {
                return new FailureApiResponse("",$"No Bid details with the bidId {Id} found");
            }
            var result = await _Context.RoomBidders.FirstOrDefaultAsync(b => b.Id == Id);


            return new SuccessApiResponse<BidDto>("Retrieved Bid Successfully", result);

        }
       
        
        public async Task<ApiResponse<object>> GetBidEntriesByUserName(string UserName)
        {
            if (UserName is null)
            {
                return new FailureApiResponse("", $"No Bid details with the bidId {UserName} found");
            }
            var result =  await _Context.RoomBidders.FirstOrDefaultAsync(b => b.UserName == UserName);


            return new SuccessApiResponse<BidDto>("Retrieved Bid Successfully", result);


        }

        public PagedResponse<BidDto> GeAllBidEntries(int pageNumber, int pageSize)
        {
            var records = _Context.RoomBidders.AsNoTracking().AsQueryable();
            var count = records.Count();
            var pagedData = records.GetPaginatedReponseAsync(pageNumber, pageSize);
            return new PagedResponse<BidDto>("Success", pagedData, count, pageNumber, pageSize);
        }

    }
}
