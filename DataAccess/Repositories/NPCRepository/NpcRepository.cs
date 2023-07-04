using AutoMapper;
using BusinessObjects.Model;
using DataAccess.GenericRepositories;
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


    }
}
