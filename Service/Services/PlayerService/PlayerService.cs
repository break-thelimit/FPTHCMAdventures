using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Repositories.InventoryRepositories;
using DataAccess.Repositories.PlayerRepositories;
using DataAccess.Repositories.SchoolRepositories;
using DataAccess.Repositories.StudentRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace Service.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IStudentRepositories _studentRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public PlayerService(IPlayerRepository playerRepository, IMapper mapper, IStudentRepositories studentRepository, ISchoolRepository schoolRepository,IInventoryRepository inventoryRepository)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;   
            _inventoryRepository = inventoryRepository;
        }

        

       /* public async Task<ServiceResponse<PlayerDto>> CheckPlayerByStudentName(string studentName)
        {
            var player = await _playerRepository.GetAsync<PlayerDto>(studentName);
            if(userId != null)
            {
                try
                {
                    List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                    {
                        x => x.Inventories,
                        x => x.ExchangeHistories,
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
                    Message = "No User",
                    StatusCode = 404,
                    Success = false
                };
            }
          
        }*/

        public async Task<ServiceResponse<Guid>> CreateNewPlayer(CreatePlayerDto createPlayerDto)
        {
            if (await _playerRepository.ExistsAsync(s => s.Passcode == createPlayerDto.Passcode))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: Player with the same passcode already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            var existingPlayerForStudent = await _playerRepository.GetSingleAsync(p => p.StudentId == createPlayerDto.StudentId);
            if (existingPlayerForStudent != null)
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: Another player is already associated with the same student.",
                    Success = false,
                    StatusCode = 400
                };
            }
            createPlayerDto.Nickname = "null";
            createPlayerDto.CreatedAt = DateTime.UtcNow;
            createPlayerDto.TotalPoint = 0;
            createPlayerDto.TotalTime = 0;
            createPlayerDto.Passcode= Guid.NewGuid().ToString("N").Substring(0, 8);
            createPlayerDto.IsPlayer = false;
            var mapper = config.CreateMapper();
            var _player = mapper.Map<Player>(createPlayerDto);
            _player.Id = Guid.NewGuid();
            await _playerRepository.AddAsync(_player);
            var studentToUpdate = await _studentRepository.GetSingleAsync(s => s.Id == createPlayerDto.StudentId);
            if (studentToUpdate != null)
            {
                studentToUpdate.PlayerId = _player.Id;
                await _studentRepository.UpdateAsync(studentToUpdate);
            }
            return new ServiceResponse<Guid>
            {
                Data = _player.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }
        public async Task<ServiceResponse<List<Guid>>> CreateNewPlayers(List<CreatePlayerDto> players)
        {
            var addedPlayerIds = new List<Guid>();
            var mapper = config.CreateMapper();

            var newPlayers = players.Select(player =>
            {
                player.Nickname = "null";
                player.CreatedAt = DateTime.UtcNow;
                player.TotalPoint = 0;
                player.TotalTime = 0;
                player.Passcode = Guid.NewGuid().ToString("N").Substring(0, 8);
                player.IsPlayer = false;

                var _player = mapper.Map<Player>(player);
                _player.Id = Guid.NewGuid();

                return _player;
            }).ToList();

            await _playerRepository.AddRangeAsync(newPlayers);

            addedPlayerIds.AddRange(newPlayers.Select(player => player.Id));
            foreach (var player in newPlayers)
            {
                var studentToUpdate = await _studentRepository.GetSingleAsync(s => s.Id == player.StudentId);
                if (studentToUpdate != null)
                {
                    studentToUpdate.PlayerId = player.Id;
                    await _studentRepository.UpdateAsync(studentToUpdate);
                }
            }
            return new ServiceResponse<List<Guid>>
            {
                Data = addedPlayerIds,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }
        public async Task<ServiceResponse<GetPlayerDto>> CheckPlayerByNickName(string nickName)
        {
            try
            {
                List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                {
                  
                    x => x.Inventories,
                    x => x.ExchangeHistories,
                    
                };
                var taskDetail = await _playerRepository.GetByWithCondition(x => x.Nickname.Equals(nickName), null, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<GetPlayerDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<GetPlayerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetPlayerDto>
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
        public async Task<ServiceResponse<IEnumerable<PlayerDto>>> GetPlayer()
        {
            var majorList = await _playerRepository.GetAllAsync<PlayerDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<PlayerDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<PlayerDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<GetPlayerDto>> GetPlayerById(Guid eventId)
        {
            try
            {

                var eventDetail = await _playerRepository.GetAsync<GetPlayerDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<GetPlayerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetPlayerDto>
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

        public async Task<ServiceResponse<GetPlayerDto>> GetPlayerByStudentId(Guid studentId)
        {
            try
            {
                List<Expression<Func<Player, object>>> includes = new List<Expression<Func<Player, object>>>
                {
                    x => x.Inventories,
                    x => x.ExchangeHistories,
                };
                var taskDetail = await _playerRepository.GetByWithCondition(x => x.StudentId == studentId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<GetPlayerDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<GetPlayerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetPlayerDto>
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

     

        public async Task<ServiceResponse<bool>> UpdatePlayer(Guid id, UpdatePlayerDto updatePlayerDto)
        {
            if (await _playerRepository.ExistsAsync(s => s.Nickname == updatePlayerDto.Nickname && s.Id != id))
            {
                return new ServiceResponse<bool>
                {
                    Message = "Duplicated data: Player with the same name already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            if (await _playerRepository.ExistsAsync(s => s.Passcode == updatePlayerDto.Passcode && s.Id != id))
            {
                return new ServiceResponse<bool>
                {
                    Message = "Duplicated data: Player with the same passcode already exists.",
                    Success = false,
                    StatusCode = 400
                };
            } 
            if (await _playerRepository.ExistsAsync(s => s.StudentId == updatePlayerDto.StudentId && s.Id != id))
            {
                return new ServiceResponse<bool>
                {
                    Message = "Duplicated data: Player with the same student already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            try
            {
                updatePlayerDto.Nickname = updatePlayerDto.Nickname.Trim();
                updatePlayerDto.Passcode = updatePlayerDto.Passcode.Trim();
                var playerToUpdate = await _playerRepository.GetSingleAsync(p => p.Id == id);
                if (playerToUpdate != null)
                {
                    // Bỏ qua cập nhật trường CreatedAt
                    updatePlayerDto.CreatedAt = playerToUpdate.CreatedAt;

                    await _playerRepository.UpdateAsync(id, updatePlayerDto);

                    return new ServiceResponse<bool>
                    {
                        Data = true,
                        Message = "Success edit",
                        Success = true,
                        StatusCode = 202
                    };
                }
                else
                {
                    return new ServiceResponse<bool>
                    {
                        Data = false,
                        Message = "Player not found",
                        Success = false,
                        StatusCode = 404
                    };
                }
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
            return await _playerRepository.Exists(id);
        }
        public async Task<ServiceResponse<IEnumerable<Player>>> GetTop5PlayerInRank()
        {
            try
            {
                var context = new db_a9c31b_capstoneContext();
                List<Player> top5playerlist = context.Players.OrderByDescending(x => x.TotalPoint).ThenBy(x => x.TotalTime).Take(5).ToList();
                return new ServiceResponse<IEnumerable<Player>>
                {
                    Data = top5playerlist,
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

        public async Task<ServiceResponse<IEnumerable<PlayerDto>>> GetRankedPlayer(Guid eventId, Guid schoolId)
        {
            try
            {

                var eventDetail = await _playerRepository.GetRankedPlayer(eventId, schoolId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<IEnumerable<PlayerDto>>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<IEnumerable<PlayerDto>>
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

        public async Task<ServiceResponse<Guid>> GetSchoolByPlayerId(Guid playerId)
        {
            try
            {
                var schoolId = await _playerRepository.GetSchoolByPlayerId(playerId);

                if (schoolId == null)
                {

                    return new ServiceResponse<Guid>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<Guid>
                {
                    Data = schoolId,
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

        public async Task<ServiceResponse<GetPlayerDto>> GetPlayerByEventId(Guid eventId)
        {
            var player = await _playerRepository.GetPlayerByEventId(eventId);
            if(player == null)
            {
                return new ServiceResponse<GetPlayerDto>
                {
                    Message = "No rows",
                    StatusCode = 200,
                    Success = true
                };
            }
            else
            {
                return new ServiceResponse<GetPlayerDto>
                {
                    Data = player,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }

        public async Task<ServiceResponse<GetPlayerDto>> GetPlayerBySchoolId(Guid schoolId)
        {
            var player = await _playerRepository.GetPlayerBySchoolId(schoolId);
            if (player == null)
            {
                return new ServiceResponse<GetPlayerDto>
                {
                    Message = "No rows",
                    StatusCode = 200,
                    Success = true
                };
            }
            else
            {
                return new ServiceResponse<GetPlayerDto>
                {
                    Data = player,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }
    }
}
