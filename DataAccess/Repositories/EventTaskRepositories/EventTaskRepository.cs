using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObjects.Model;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Exceptions;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.EventRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EventTaskRepositories
{
    public class EventTaskRepository : GenericRepository<EventTask>, IEventTaskRepository
    {
        private readonly db_a9c31b_capstoneContext _dbContext;
        private readonly IMapper _mapper;

        public EventTaskRepository(db_a9c31b_capstoneContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventTaskDto>> GetEventTaskByEventId(Guid eventId)
        {
            var eventTasks = await _dbContext.EventTasks
                .Where(x => x.EventId == eventId)
                .ToListAsync();

            var eventTaskDtos = _mapper.Map<IEnumerable<EventTaskDto>>(eventTasks);
            return eventTaskDtos;
        }
    }
}
