using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObjects.Model;
using DataAccess.Dtos.EventDto;
using DataAccess.Exceptions;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EventRepositories
{
    public class EventRepositories : GenericRepository<Event>, IEventRepositories
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public EventRepositories(FPTHCMAdventuresDBContext dbContext,IMapper mapper) : base(dbContext,mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EventDto> GetDetailEvents(Guid id)
        {
            var country = await _dbContext.Events.Include(q => q.EventTasks).Include(a => a.SchoolEvents)
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetDetailEvents), id);
            }

            return country;
        }
    }
}


