using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Repositories.PlayerHistoryRepositories;
using DataAccess.Repositories.PlayerRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewPlayer(CreatePlayerDto createPlayerDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Player>(createPlayerDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _playerRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetPlayerDto>>> GetPlayer()
        {
            var majorList = await _playerRepository.GetAllAsync<GetPlayerDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetPlayerDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetPlayerDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<PlayerDto>> GetPlayerById(Guid eventId)
        {
            try
            {

                var eventDetail = await _playerRepository.GetAsync<PlayerDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<PlayerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<PlayerDto>
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

        public async Task<ServiceResponse<string>> UpdatePlayer(Guid id, UpdatePlayerDto PlayerDto)
        {
            try
            {
                await _playerRepository.UpdateAsync(id, PlayerDto);
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
            return await _playerRepository.Exists(id);
        }
       
    }
}
