using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.PrizeDto;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.ExchangeHistoryRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PrizeRepositories
{
    public class PrizeRepository : GenericRepository<Prize>, IPrizeRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public PrizeRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
      

    }
}