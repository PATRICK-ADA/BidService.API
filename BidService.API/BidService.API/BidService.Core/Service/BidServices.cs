using BidService.API.Abstraction;
using BidService.API.BidService.Core.ApiResponse;
using BidService.API.BidService.Core.Utility.Common;
using BidService.API.BidService.Domain.RequestDto;
using BidService.Core.Abstraction;


namespace BidService.API.Service
{
    public class BidServices : IBidService
    {
        private readonly IBidRepository _bidRepository;



        public BidServices(IBidRepository bidRepository)
        {

            _bidRepository = bidRepository;
        }

        public async Task<ApiResponse<object>> GetBidEntriesById(Guid Id)
        {
           
            return await _bidRepository.GetBidEntriesById(Id);
        }

        public async Task<ApiResponse<object>> GetBidEntriesByUserName(string UserName)
        {
           
            return await _bidRepository.GetBidEntriesByUserName(UserName);

        }
       public  PagedResponse<BidDto> GetAllBidEntries(int pageNumber, int pageSize)
        {

           return _bidRepository.GeAllBidEntries(pageNumber, pageSize);
           
        }


    }
}
