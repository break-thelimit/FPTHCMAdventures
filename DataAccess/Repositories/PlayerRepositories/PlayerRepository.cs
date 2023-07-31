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
        private readonly db_a9c31b_capstoneContext _dbContext;
        private readonly IMapper _mapper;

        public PlayerRepository(db_a9c31b_capstoneContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        
    }
}