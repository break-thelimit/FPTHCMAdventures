using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PrizeDto;
using DataAccess.Repositories.PrizeRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services.PrizeService
{
    public class PrizeService : IPrizeService
    {
        private readonly IPrizeRepository _prizeRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public PrizeService(IPrizeRepository prizeRepository, IMapper mapper)
        {
            _prizeRepository = prizeRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewPrize(CreatePrizeDto createGiftDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Prize>(createGiftDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _prizeRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<PrizeDto>>> GetPrize()
        {
            var giftList = await _prizeRepository.GetAllAsync<PrizeDto>();

            if (giftList != null)
            {
                return new ServiceResponse<IEnumerable<PrizeDto>>
                {
                    Data = giftList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<PrizeDto>>
                {
                    Data = giftList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<string>> GetTotalPrize()
        {
            var context = new FPTHCMAdventuresDBContext();
            try
            {
                long total = context.Prizes.Count();
                return new ServiceResponse<string>
                {
                    Data = total.ToString(),
                    Message = "Success!",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new ServiceResponse<string>
                {
                    Message = ex.ToString(),
                    Success = false,
                    StatusCode = 500
                };
            }

        }

        public async Task<ServiceResponse<PrizeDto>> GetPrizeById(Guid eventId)
        {
            try
            {

                var eventDetail = await _prizeRepository.GetAsync<PrizeDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<PrizeDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<PrizeDto>
                {
                    Data = eventDetail,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> UpdatePrize(Guid id, UpdatePrizeDto giftDto)
        {
            try
            {
                giftDto.Id = id;
               
                await _prizeRepository.UpdateAsync(id, giftDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PrizeExists(id))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Invalid Record Id",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    throw;
                }
            }
        }

       

        private async Task<bool> PrizeExists(Guid id)
        {
            return await _prizeRepository.Exists(id);
        }

        public async Task<ServiceResponse<string>> DisablePrize(Guid id)
        {
            var checkEvent = await _prizeRepository.GetAsync<PrizeDto>(id);

            if (checkEvent == null)
            {
                return new ServiceResponse<string>
                {
                    Data = "null",
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
            else
            {
                checkEvent.Status = "INACTIVE";
/*                await _prizeRepository.UpdateAsync(id, checkEvent);
*/                return new ServiceResponse<string>
                {
                    Data = checkEvent.Status,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }
    }
}