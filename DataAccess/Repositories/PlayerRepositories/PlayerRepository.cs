using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.PlayerDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PlayerRepositories
{
    public class PlayerRepository : GenericRepository<Player> , IPlayerRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public PlayerRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPlayerDto>> GetRankedPlayer(Guid eventid, Guid schoolId)
        {
            var ranked= await _dbContext.Players.Include(s=>s.Student.School).Where(p=>p.EventId.Equals(eventid) && p.Student.SchoolId.Equals(schoolId)).
                OrderByDescending(x => x.TotalPoint).ThenBy(x => x.TotalTime).Select(x=> new GetPlayerDto 
                {
                    Id=x.Id,
                    Nickname=x.Nickname,
                    CreatedAt=x.CreatedAt,
                    TotalPoint=x.TotalPoint,
                    TotalTime=x.TotalTime,
                    Isplayer=x.Isplayer
                }).ToListAsync();
            return ranked;
        }
    }
}