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
            if (await _playerPrizeRepository.ExistsAsync(s => s.PlayerId == createPlayerPrizeDto.PlayerId))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: Prize with the same player already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
           
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

        public async Task<ServiceResponse<bool>> UpdatePlayerPrize(Guid id, UpdatePlayerPrizeDto PlayerPrizeDto)
        {
            try
            {

                await _playerPrizeRepository.UpdateAsync(id, PlayerPrizeDto);
                return new ServiceResponse<bool>
                {   
                    Data = true,
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PlayerPrizeExists(id))
                {
                    return new ServiceResponse<bool>
                    {
                        Data = false,
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
