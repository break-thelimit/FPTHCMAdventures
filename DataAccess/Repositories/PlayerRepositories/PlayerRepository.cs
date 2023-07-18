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

        public async Task<List<GetPlayerWithUserNameDto>> GetAllPlayerAsync()
        {
            var playerlist1 = await _dbContext.Players.Include(x => x.User).Select(x => new GetPlayerWithUserNameDto
            {
                Id = x.Id,
                UserId = x.UserId,
                UserName= x.User.Fullname,
                TotalPoint=x.TotalPoint,
                TotalTime=x.TotalTime,
                NickName=x.Nickname
            }).ToListAsync();
            return playerlist1;
        }
    }
}