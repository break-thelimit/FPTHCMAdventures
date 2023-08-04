using BusinessObjects.Model;
using DataAccess.Dtos.PlayerDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<ServiceResponse<IEnumerable<GetPlayerDto>>> GetPlayer();
        Task<ServiceResponse<PlayerDto>> GetPlayerById(Guid eventId);
        Task<ServiceResponse<PlayerDto>> GetPlayerByStudentId(Guid studentId);        
        Task<ServiceResponse<GetPlayerDto>> CheckPlayerByNickName(string nickName);
        Task<ServiceResponse<Guid>> CreateNewPlayer(CreatePlayerDto createPlayerDto);
        Task<ServiceResponse<string>> UpdatePlayer(Guid id, UpdatePlayerDto PlayerDto);
        Task<ServiceResponse<IEnumerable<Player>>> GetTop5PlayerInRank();
        Task<ServiceResponse<IEnumerable<GetPlayerDto>>> GetRankedPlayer(Guid eventId, Guid schoolId);
    }
}
