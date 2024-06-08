using BidService.API.BidService.Core.ApiResponse;
using BidService.API.BidService.Core.Utility.Common;
using BidService.API.BidService.Domain.Entities;
using BidService.API.BidService.Domain.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidService.Core.Abstraction
{
    public interface IBidRepository
    {
        Task<ApiResponse<object>> GetBidEntriesById(Guid Id);
        Task<ApiResponse<object>> GetBidEntriesByUserName(string UserName);
        PagedResponse<BidDto> GeAllBidEntries(int pageNumber, int pageSize);


    }
}
