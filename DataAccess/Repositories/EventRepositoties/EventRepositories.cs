using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessObjects.Model;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Exceptions;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EventRepositories
{
    public class EventRepositories : GenericRepository<Event>, IEventRepositories
    {
        private readonly db_a9c31b_capstoneContext _dbContext;
        private readonly IMapper _mapper;

        public EventRepositories(db_a9c31b_capstoneContext dbContext,IMapper mapper) : base(dbContext,mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetTaskAndEventDto>> GetTaskAndEventListByTimeNow()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime vietnamTime = ConvertToVietnamTime(utcNow);
            var status = "ACTIVE";

            var query = _dbContext.Events
                .Include(e => e.EventTasks).ThenInclude(et => et.Task)
                .Include(e => e.EventTasks).ThenInclude(et => et.Task.Location)
                .Include(e => e.EventTasks).ThenInclude(et => et.Task.Major)
                .Include(e => e.EventTasks).ThenInclude(et => et.Task.Npc)
                .Include(e => e.EventTasks).ThenInclude(et => et.Task.Item)
                .Where(e => e.StartTime <= vietnamTime && e.EndTime > vietnamTime && e.Status == "ACTIVE");

            var getTaskAndEventDtos = await query
                .Select(e => new GetTaskAndEventDto
                {
                    EventName = e.Name,
                    TaskDtos = e.EventTasks
                        .Where(et => et.Task.Status == "ACTIVE") // Lọc theo trạng thái "active"
                        .Select(et => new GetTaskRequestDto
                        {
                            Id = et.Task.Id,
                            EventtaskId = et.Id,
                            Name = et.Task.Name,
                            ItemName = et.Task.Item.Name,
                            LocationName = et.Task.Location.LocationName,
                            MajorName = et.Task.Major.Name,
                            MajorId = et.Task.MajorId,
                            NpcName = et.Task.Npc.Name,
                            Point = et.Point,
                            Status = et.Task.Status,
                            Type = et.Task.Type,
                            Starttime = et.StartTime, 
                            Endtime = et.EndTime,     
                        })
                        .ToList(),
                    StartTime = e.StartTime,
                    EndTime = e.EndTime
                })
                .ToListAsync();

            if (getTaskAndEventDtos.Count == 0 || getTaskAndEventDtos.Any(dto => dto.EventName == null) || getTaskAndEventDtos.Any(dto => dto.TaskDtos.Count() == 0))
            {
                return new List<GetTaskAndEventDto>();
            }

            return getTaskAndEventDtos;
        }

        public DateTime ConvertToVietnamTime(DateTime dateTimeUtc)
        {
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, vietnamTimeZone);
            return vietnamTime;
        }
    }
} 


