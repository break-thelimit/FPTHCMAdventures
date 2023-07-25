using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.GiftDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.GiftService
{
    public interface IGiftService
    {
        Task<ServiceResponse<IEnumerable<GetGiftDto>>> GetGift();
        Task<ServiceResponse<GiftDto>> GetGiftById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewGift(CreateGiftDto createGiftDto);
        Task<ServiceResponse<string>> UpdateGift(Guid id, UpdateGiftDto giftDto);
<<<<<<< HEAD

        Task<ServiceResponse<string>> GetTotalGift();

=======
        Task<ServiceResponse<string>> GetTotalGift();
>>>>>>> origin/main
    }
}
