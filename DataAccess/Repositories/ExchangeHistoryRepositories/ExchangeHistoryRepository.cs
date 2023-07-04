using AutoMapper;
using BusinessObjects.Model;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.EventTaskRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ExchangeHistoryRepositories
{
    public class ExchangeHistoryRepository : GenericRepository<ExchangeHistory>, IExchangeHistoryRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public ExchangeHistoryRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


    }
}