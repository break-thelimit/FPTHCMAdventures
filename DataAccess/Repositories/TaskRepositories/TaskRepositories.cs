using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.TaskDto;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.EventRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BusinessObjects.Model.Task;

namespace DataAccess.Repositories.TaskRepositories
{
    public class TaskRepositories : GenericRepository<Task>, ITaskRepositories
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public TaskRepositories(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetTaskDto>> GetAllTaskAsync()
        {
            var ranklist1 = await _dbContext.Tasks.Include(x => x.Major).Include(l=>l.Location).Include(n=>n.Npc).Select(x => new GetTaskDto
            {
                Id = x.Id,
                LocationId=x.LocationId,
                LocationName=x.Location.LocationName,
                MajorId = x.MajorId,
                MajorName = x.Major.Name,
                NpcId=x.NpcId,
                NpcName=x.Npc.NpcName,
                EndTime=x.EndTime,
                IsRequireitem=x.IsRequireitem,
                TimeOutAmount=x.TimeOutAmount,
                ActivityName=x.ActivityName,
                Point=x.Point,
                Type=x.Type,
                Status=x.Status
            }).ToListAsync();
            return ranklist1;
        }
    }
}
