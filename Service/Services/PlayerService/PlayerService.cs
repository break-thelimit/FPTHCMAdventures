﻿using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.UserDto;
using DataAccess.Repositories.PlayerHistoryRepositories;
using DataAccess.Repositories.PlayerRepositories;
using DataAccess.Repositories.SchoolRepositories;
using DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public PlayerService(IPlayerRepository playerRepository, IMapper mapper,IUserRepository userRepository, ISchoolRepository schoolRepository)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _schoolRepository = schoolRepository;   
        }

        public async Task<ServiceResponse<PlayerDto>> CheckPlayerByUserName(string username)
        {
            var userId = await _userRepository.GetUserIdByUserName(username);
            if(userId != null)
            {
                try
                {
                    List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                {
                    x => x.Ranks,
                    x => x.Inventories,
                    x => x.ExchangeHistories,
                    x => x.PlayHistories
                };
                    var taskDetail = await _playerRepository.GetByWithCondition(x => x.UserId == userId, includes, true);
                    var _mapper = config.CreateMapper();
                    var taskDetailDto = _mapper.Map<PlayerDto>(taskDetail);
                    if (taskDetail == null)
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
            else
            {
                return new ServiceResponse<PlayerDto>
                {
                    Message = "No User ",
                    StatusCode = 404,
                    Success = false
                };
            }
          
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

        public async Task<ServiceResponse<PlayerDto>> GetPlayerByUserId(Guid userId)
        {
            try
            {
                List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                {
                    x => x.Ranks,
                    x => x.Inventories,
                    x => x.ExchangeHistories,
                    x => x.PlayHistories
                };
                var taskDetail = await _playerRepository.GetByWithCondition(x => x.UserId == userId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<PlayerDto>(taskDetail);
                if (taskDetail == null)
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

        public async Task<ServiceResponse<IEnumerable<GetPlayerWithUserNameDto>>> GetPlayerWithUserName()
        {
            var userList = await _playerRepository.GetAllPlayerAsync();

            if (userList != null)
            {
                return new ServiceResponse<IEnumerable<GetPlayerWithUserNameDto>>
                {
                    Data = userList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetPlayerWithUserNameDto>>
                {
                    Data = userList,
                    Success = false,
                    Message = "Faile because List user null",
                    StatusCode = 200
                };
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
                if (!await PlayerExists(id))
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

        public async Task<ServiceResponse<IEnumerable<Player>>> GetTop5PlayerInRank()
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                List<Player> top5playerlist= context.Players.OrderByDescending(x => x.TotalPoint).ThenBy(x => x.TotalTime).Take(5).ToList();
                return new ServiceResponse<IEnumerable<Player>>
                {
                    Data= top5playerlist,
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                    return new ServiceResponse<IEnumerable<Player>>
                    {
                        Message = "Invalid Record Id",
                        Success = false,
                        StatusCode = 500
                    };
            }
        }
        private async Task<bool> PlayerExists(Guid id)
        {
            return await _playerRepository.Exists(id);
        }
       
    }
}
