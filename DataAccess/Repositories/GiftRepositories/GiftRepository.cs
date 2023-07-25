using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.GiftDto;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.ExchangeHistoryRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.GiftRepositories
{
    public class GiftRepository : GenericRepository<Gift>, IGiftRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public GiftRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<GetGiftDto>> GetAllGiftAsync()
        {
            var ranklist1 = await _dbContext.Gifts.Include(x => x.Rank).Select(x => new GetGiftDto
            {
                Id = x.Id,
                RankNumber=x.Rank.RankNumber,
                RankId=x.RankId,
                GiftName=x.GiftName,
                Decription=x.Decription,
                Price=x.Price
            }).ToListAsync();
            return ranklist1;
        }

    }
}