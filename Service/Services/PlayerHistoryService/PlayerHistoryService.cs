using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Repositories.ItemRepositories;
using DataAccess.Repositories.PlayerHistoryRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PlayerHistoryService
{
    public class PlayerHistoryService : IPlayerHistoryService
    {
        private readonly IPlayerHistoryRepository _playerHistoryRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public PlayerHistoryService(IPlayerHistoryRepository playerHistoryRepository, IMapper mapper)
        {
            _playerHistoryRepository = playerHistoryRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewPlayerHistory(CreatePlayerHistoryDto createPlayerHistoryDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<PlayHistory>(createPlayerHistoryDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _playerHistoryRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetPlayerHistoryDto>>> GetPlayerHistory()
        {
            var majorList = await _playerHistoryRepository.GetAllAsync<GetPlayerHistoryDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetPlayerHistoryDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetPlayerHistoryDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<PlayerHistoryDto>> GetPlayerHistoryById(Guid eventId)
        {
            try
            {

                var eventDetail = await _playerHistoryRepository.GetAsync<PlayerHistoryDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<PlayerHistoryDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<PlayerHistoryDto>
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

        public async Task<ServiceResponse<string>> UpdatePlayerHistory(Guid id, UpdatePlayerHistoryDto PlayerHistoryDto)
        {
            try
            {
                await _playerHistoryRepository.UpdateAsync(id, PlayerHistoryDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventTaskExists(id))
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
        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _playerHistoryRepository.Exists(id);
        }

        
    }
}
