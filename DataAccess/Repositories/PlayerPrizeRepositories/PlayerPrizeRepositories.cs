using AutoMapper;
using BusinessObjects.Model;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.PlayerHistoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PlayerPrizeRepositories
{
    public class PlayerPrizeRepositories : GenericRepository<PlayerPrize>, IPlayerPrizeRepositories
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public PlayerPrizeRepositories(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

    }
}