using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Dtos.PlayerPrizeDto;
using DataAccess.Repositories.PlayerHistoryRepositories;
using DataAccess.Repositories.PlayerPrizeRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PlayerPrizeService
{
    public class PlayerPrizeService : IPlayerPrizeService
    {
        private readonly IPlayerPrizeRepositories _playerPrizeRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public PlayerPrizeService(IPlayerPrizeRepositories playerPrizeRepository, IMapper mapper)
        {
            _playerPrizeRepository = playerPrizeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Guid>> CreateNewPlayerPrize(CreatePlayerPrizeDto createPlayerPrizeDto)
        {
            var majorList = await _playerPrizeRepository.GetAllAsync<GetPlayerPrizeDto>();
            if (majorList != null)
            {
                var playerHistory = majorList.FirstOrDefault(x => x.PlayerId == createPlayerPrizeDto.PlayerId );
                if (playerHistory != null)
                {
                    return new ServiceResponse<Guid>
                    {
                        Message = "Failed taskId have exists",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    var mapper = config.CreateMapper();
                    var eventTaskcreate = mapper.Map<PlayerPrize>(createPlayerPrizeDto);
                    eventTaskcreate.Id = Guid.NewGuid();
                    await _playerPrizeRepository.AddAsync(eventTaskcreate);

                    return new ServiceResponse<Guid>
                    {
                        Data = eventTaskcreate.Id,
                        Message = "Successfully",
                        Success = true,
                        StatusCode = 201
                    };
                }
            }
            else
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Null",
                    Success = false,
                    StatusCode = 500
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<PlayerPrizeDto>>> GetPlayerPrize()
        {
            var playerhistoryList = await _playerPrizeRepository.GetAllAsync<PlayerPrizeDto>();

            if (playerhistoryList != null)
            {
                return new ServiceResponse<IEnumerable<PlayerPrizeDto>>
                {
                    Data = playerhistoryList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<PlayerPrizeDto>>
                {
                    Data = playerhistoryList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<GetPlayerPrizeDto>> GetPlayerPrizeById(Guid prizeId)
        {
            try
            {

                var eventDetail = await _playerPrizeRepository.GetAsync<GetPlayerPrizeDto>(prizeId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<GetPlayerPrizeDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetPlayerPrizeDto>
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

        public async Task<ServiceResponse<string>> UpdatePlayerPrize(Guid id, UpdatePlayerPrizeDto PlayerPrizeDto)
        {
            try
            {
                PlayerPrizeDto.Id = id;
                await _playerPrizeRepository.UpdateAsync(id, PlayerPrizeDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PlayerPrizeExists(id))
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

        private async Task<bool> PlayerPrizeExists(Guid id)
        {
            return await _playerPrizeRepository.Exists(id);
        }
    }
}
