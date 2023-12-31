﻿using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Repositories.EventRepositories;
using DataAccess.Repositories.EventTaskRepositories;
using DataAccess.Repositories.TaskRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.EventTaskService
{
    public class EventTaskService : IEventTaskService
    {
        private readonly IEventTaskRepository _eventTaskRepository;
        private readonly IEventRepositories _eventRepository;
        private readonly ITaskRepositories _taskRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public EventTaskService(IEventTaskRepository eventTaskRepository, IMapper mapper, IEventRepositories eventRepositories, ITaskRepositories taskRepository)
        {
            _eventTaskRepository = eventTaskRepository;
            _eventRepository = eventRepositories;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewEventTask(CreateEventTaskDto createEventTaskDto)
        {
            var existingEvent = await _eventRepository.GetAsync(createEventTaskDto.EventId);
            if (existingEvent == null)
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Event does not exist.",
                    Success = false,
                    StatusCode = 400
                };
            }

            // Kiểm tra xem có công việc nào trùng EventId và TaskId đã tồn tại trong sự kiện không
            if (await _eventTaskRepository.ExistsAsync(t => t.EventId == createEventTaskDto.EventId && t.Id == createEventTaskDto.TaskId))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "A task with the same EventId and TaskId already exists in the event.",
                    Success = false,
                    StatusCode = 400
                };
            }

            // Kiểm tra xem thời gian StartTime và EndTime của công việc nằm trong khoảng thời gian của sự kiện
            if (createEventTaskDto.StartTime < existingEvent.StartTime || createEventTaskDto.EndTime > existingEvent.EndTime)
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Task's StartTime and EndTime must be within the event's time range.",
                    Success = false,
                    StatusCode = 400
                };
            }
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<EventTask>(createEventTaskDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _eventTaskRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetEventTaskDto>>> GetEventTask()
        {
            var eventList = await _eventTaskRepository.GetAllAsync<GetEventTaskDto>();
           
            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<GetEventTaskDto>>
                {
                    Data = eventList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetEventTaskDto>>
                {
                    Data = eventList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<EventTaskDto>> GetEventById(Guid eventId)
        {
            try
            {
                var eventDetail = await _eventTaskRepository.GetAsync<EventTaskDto>(eventId);
                if (eventDetail == null)
                {

                    return new ServiceResponse<EventTaskDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EventTaskDto>
                {
                    Data = eventDetail,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
      

        public async Task<ServiceResponse<bool>> UpdateTaskEvent(Guid id, UpdateEventTaskDto eventTaskDto)
        {
            var existingEventTask = await _eventTaskRepository.GetAsync(id);
            if (existingEventTask == null)
            {
                return new ServiceResponse<bool>
                {
                    Message = "Task does not exist.",
                    Success = false,
                    StatusCode = 400
                };
            }
            // Kiểm tra xem sự kiện tồn tại hay không
            var existingEvent = await _eventRepository.GetAsync(eventTaskDto.EventId);
            if (existingEvent == null)
            {
                return new ServiceResponse<bool>
                {
                    Message = "Event does not exist.",
                    Success = false,
                    StatusCode = 400
                };
            }
            if (existingEventTask.EventId != eventTaskDto.EventId)
            {
                return new ServiceResponse<bool>
                {
                    Message = "Cannot change EventId while updating EventTask.",
                    Success = false,
                    StatusCode = 400
                };
            }
            // Kiểm tra xem có công việc nào trùng EventId và TaskId đã tồn tại trong sự kiện không
            var isDuplicateEventTask = await _eventTaskRepository.ExistsAsync(t =>
                t.EventId == eventTaskDto.EventId && t.TaskId == eventTaskDto.TaskId && t.Id != id);

            if (isDuplicateEventTask)
            {
                return new ServiceResponse<bool>
                {
                    Message = "A task with the same EventId and TaskId already exists in the event.",
                    Success = false,
                    StatusCode = 400
                };
            }

            // Kiểm tra xem thời gian StartTime và EndTime của công việc nằm trong khoảng thời gian của sự kiện
            if (eventTaskDto.StartTime < existingEvent.StartTime || eventTaskDto.EndTime > existingEvent.EndTime)
            {
                return new ServiceResponse<bool>
                {
                    Message = "Task's StartTime and EndTime must be within the event's time range.",
                    Success = false,
                    StatusCode = 400
                };
            }
            
            try
            {
                existingEventTask.StartTime = eventTaskDto.StartTime;
                existingEventTask.EndTime = eventTaskDto.EndTime;
                existingEventTask.Priority = eventTaskDto.Priority;
                existingEventTask.Point = eventTaskDto.Point;

                await _eventTaskRepository.UpdateAsync(id, existingEventTask);
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventTaskExists(id))
                {
                    return new ServiceResponse<bool>
                    {
                        Data = false,
                        Message = "Invalid Record Id",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _eventTaskRepository.Exists(id);
        }

        public async Task<ServiceResponse<EventTaskDto>> GetEventTaskByTaskId(Guid taskId)
        {
            try
            {
                List<Expression<Func<EventTask, object>>> includes = new List<Expression<Func<EventTask, object>>>
                {
                   

                };
                var taskDetail = await _eventTaskRepository.GetByWithCondition(x => x.TaskId == taskId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<EventTaskDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<EventTaskDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EventTaskDto>
                {
                    Data = taskDetailDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetTaskByEventIdDto>>> GetTaskByEventTaskWithEventId(Guid eventId)
        {
            var taskList = await _eventTaskRepository.GetTaskByEventTaskWithEventId(eventId);


            if (taskList.Any())
            {
                return new ServiceResponse<IEnumerable<GetTaskByEventIdDto>>
                {
                    Data = taskList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetTaskByEventIdDto>>
                {
                    Data = taskList,
                    Success = false,
                    Message = "Failed because List task null",
                    StatusCode = 200
                };
            }
        }
    }
}
