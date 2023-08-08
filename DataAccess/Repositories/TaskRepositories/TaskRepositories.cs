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

/*        public async Task<IEnumerable<TaskDto>> GetTaskByEventTaskWithEventId(Guid EventId)
        {
            var eventtask = await _dbContext.EventTasks.Where(x => x.EventId == EventId).ToListAsync();
            if(eventtask.Any() || eventtask == null)
            {
                return null;
            }
            else
            {
                foreach (var item in eventtask)
                {
                    var task = await _dbContext.Tasks.Where(x => x.Id == item.TaskId).ToListAsync();
                    var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(task);

                    return taskDtos;
                }
            }
            return null;
        }*/

        public async Task<IEnumerable<TaskDto>> GetTaskByEventTaskWithEventId(Guid EventId)
        {
            var eventtask = await _dbContext.EventTasks.Include(e => e.Task).Where(x => x.EventId == EventId).Select(t => new TaskDto
            {
                Id = t.Task.Id,
                LocationName = t.Task.Location.LocationName,
                MajorName=t.Task.Major.Name,
                NpcName=t.Task.Npc.Name,
                ItemName=t.Task.Item.Name,
                Name=t.Task.Name,
                Point=t.Task.Point,
                Type=t.Task.Type,
                Status=t.Task.Status
            }).ToListAsync();
            var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(eventtask);
            return taskDtos;
        }
    }
}
