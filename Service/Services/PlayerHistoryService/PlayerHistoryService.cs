﻿using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.NPCDto;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Repositories.ItemRepositories;
using DataAccess.Repositories.PlayerHistoryRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var playerHistoryDtos = await _playerHistoryRepository.GetAllAsync<GetPlayerHistoryDto>();
            if (playerHistoryDtos != null)
            {
               var playerHistory = playerHistoryDtos.FirstOrDefault(x => x.PlayerId == createPlayerHistoryDto.PlayerId && x.EventtaskId == createPlayerHistoryDto.EventtaskId);
                if(playerHistory != null)
                {
                    return new ServiceResponse<Guid>
                    {
                        Message = "Failed taskId have exists",
                        Success = false,
                        StatusCode = 400
                    };
                }
                else
                {
                    createPlayerHistoryDto.Status = createPlayerHistoryDto.Status.Trim();

                    var mapper = config.CreateMapper();
                    var eventTaskcreate = mapper.Map<PlayerHistory>(createPlayerHistoryDto);
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

        public async Task<ServiceResponse<bool>> DisablePlayerHistory(Guid id)
        {
            var checkEvent = await _playerHistoryRepository.GetAsync<PlayerHistoryDto>(id);

            if (checkEvent == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Failed",
                    StatusCode = 400,
                    Success = false
                };
            }
            else
            {
                /*checkEvent.Status = "INACTIVE";
                await _playerHistoryRepository.UpdateAsync(id, checkEvent);*/
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetPlayerHistoryDto>>> GetPlayerHistory()
        {
            var playerhistoryList = await _playerHistoryRepository.GetAllAsync<GetPlayerHistoryDto>();

            if (playerhistoryList != null)
            {
                return new ServiceResponse<IEnumerable<GetPlayerHistoryDto>>
                {
                    Data = playerhistoryList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetPlayerHistoryDto>>
                {
                    Data = playerhistoryList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<GetPlayerHistoryDto>> GetPlayerHistoryById(Guid eventId)
        {
            try
            {

                var eventDetail = await _playerHistoryRepository.GetAsync<GetPlayerHistoryDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<GetPlayerHistoryDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetPlayerHistoryDto>
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

        public async Task<ServiceResponse<PlayerHistoryDto>> GetPlayerHistoryByEventTaskId(Guid eventTaskId)
        {
            try
            {
                List<Expression<Func<PlayerHistory, object>>> includes = new List<Expression<Func<PlayerHistory, object>>>
                {
                   
                };
                var taskDetail = await _playerHistoryRepository.GetByWithCondition(x => x.EventtaskId == eventTaskId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<PlayerHistoryDto>(taskDetail);
               
                if (taskDetail == null)
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
                    Data = taskDetailDto,
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

        public async Task<ServiceResponse<GetPlayerHistoryDto>> GetPlayerHistoryByEventTaskIdAndPlayerId(Guid taskId, Guid PlayerId)
        {
            try
            {
                var playerHistory = await _playerHistoryRepository.GetPlayerHistoryByEventTaskIdAndPlayerId(taskId, PlayerId);
               
                if (playerHistory == null)
                {

                    return new ServiceResponse<GetPlayerHistoryDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetPlayerHistoryDto>
                {
                    Data = playerHistory,
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

        public async Task<ServiceResponse<bool>> UpdatePlayerHistory(Guid id, UpdatePlayerHistoryDto PlayerHistoryDto)
        {
            try
            {
                PlayerHistoryDto.Status = PlayerHistoryDto.Status.Trim();
                await _playerHistoryRepository.UpdateAsync(id, PlayerHistoryDto);
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
                if (!await EventTaskExists(id))
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
        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _playerHistoryRepository.Exists(id);
        }

        
    }
}
