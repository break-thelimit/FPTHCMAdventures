using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.NPCDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.NPCRepository
{
    public class NpcRepository : GenericRepository<Npc>, INpcRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public NpcRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<GetNpcDto>> GetAllNpckAsync()
        {
            var ranklist1 = await _dbContext.Npcs.Include(l => l.Question).Select(x => new GetNpcDto
            {
                Id = x.Id,
                QuestionId=x.QuestionId,
                QuestionName=x.Question.QuestionName,
                NpcName=x.NpcName,
                Introduce=x.Introduce,
                Status=x.Status
            }).ToListAsync();
            return ranklist1;
        }

    }
}
