using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.EventDto;
using DataAccess.Exceptions;
using DataAccess.Repositories.EventRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IEventRepositories _eventRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public EventService(IEventRepositories eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Guid>> CreateNewEvent(CreateEventDto createEventDto)
        {
            var mapper = config.CreateMapper();
            var eventcreate = mapper.Map<Event>(createEventDto);
            eventcreate.Id = Guid.NewGuid();
            await _eventRepository.AddAsync(eventcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetEventDto>>> GetEvent()
        {
            var eventList = await _eventRepository.GetAllAsync();
            var _mapper = config.CreateMapper();
            var lstDto = _mapper.Map<List<GetEventDto>>(eventList);
            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<GetEventDto>>
                {
                    Data = lstDto,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetEventDto>>
                {
                    Data = lstDto,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<EventDto>> GetEventById(Guid eventId)
        {
            try
            {
                List<Expression<Func<Event, object>>> includes = new List<Expression<Func<Event, object>>>
                {
                    x => x.SchoolEvents,
                    x => x.EventTasks,
                };
                var eventDetail = await _eventRepository.GetByWithCondition(x => x.Id == eventId, includes, true);
                var _mapper = config.CreateMapper();
                var eventDetailDto = _mapper.Map<EventDto>(eventDetail);
                if (eventDetail == null)
                {

                    return new ServiceResponse<EventDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<EventDto>
                {
                    Data = eventDetailDto,
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

        public async Task<ServiceResponse<string>> UpdateEvent(Guid id, UpdateEventDto eventDto)
        {
            if (id != eventDto.Id)
            {
                return new ServiceResponse<string>
                {
                    Message = "Invalid Record Id",
                    Success = false,
                    StatusCode = 500
                };
                
            }
            var checkEvent = await _eventRepository.GetAsync(id);
            if(checkEvent == null)
            {
                return new ServiceResponse<string>
                {
                    Message = "No rows",
                    Success = false,
                    StatusCode = 500
                };
            }
            else
            {
                _mapper.Map(eventDto, checkEvent);

                try
                {
                    await _eventRepository.UpdateAsync(checkEvent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CountryExists(id))
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "No rows",
                            Success = false,
                            StatusCode = 500
                        };
                    }
                    else
                    {
                        throw;
                    }
                }
                return new ServiceResponse<string>
                {
                    Message = "Update Success",
                    Success = true,
                    StatusCode = 500
                };
            }
        }

        private async Task<bool> CountryExists(Guid id)
        {
            return await _eventRepository.Exists(id);
        }
    }
}
