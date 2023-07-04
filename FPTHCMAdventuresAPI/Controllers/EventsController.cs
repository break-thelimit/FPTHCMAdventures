using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Model;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Service;
using System.Net;
using Service.Services.EventService;
using AutoMapper;
using DataAccess.Dtos.EventDto;
using DataAccess.Exceptions;

namespace XavalorAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        IEventRepository eventRepository = new EventRepository();
        private readonly IConfiguration Configuration;
        public EventsController(IMapper mapper, IConfiguration config,IEventService eventService)
        {
            this._mapper = mapper;

            Configuration = config;
            _eventService = eventService;
        }
/*
        // GET: api/Events
        [HttpGet]
        public JsonResult GetEvents()
        {
            IEnumerable<Event> list = eventRepository.GetEvents();
            return new JsonResult(new
            {
                result = list
            });
        }

*//*        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }*//*

        // PUT: api/Events/5
        [HttpPut]
        public JsonResult UpdateEvent([FromForm] Event event1)
        {
            Event tmp = eventRepository.UpdateEvent(event1);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // created successfully!
                    product_id = tmp.Id,
                    message = "Updated successfully!"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = 406, // created failed with invalid input value!
                    message = "This event name has existed in system!"
                });
            }
        }

        // POST: api/Events
        [HttpPost]
        public JsonResult CreateEvent([FromForm] Event event1)
        {
            Event tmp = eventRepository.CreateEvent(event1);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // created successfully!
                    event_id = tmp.Id,
                    message = "Created successfully!"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = 406,
                    message = "Created failed!"
                });
            }
        }
*/

        //Phan cua kiet
        [HttpGet(Name = "GetEventList")]
        
        public async Task<ActionResult<ServiceResponse<GetEventDto>>> GetEventList()
        {
            try
            {
                var res = await _eventService.GetEvent();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventById(Guid id)
        {
            var eventDetail = await _eventService.GetEventById(id);
            return Ok(eventDetail);
        }

        [HttpPost("event", Name = "CreateNewEvent")]

        public async Task<ActionResult<ServiceResponse<EventDto>>> CreateNewEvent(CreateEventDto eventDto)
        {
            try
            {
                var res = await _eventService.CreateNewEvent(eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        } 
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<EventDto>>> CreateNewEvent(Guid id, [FromBody] UpdateEventDto eventDto)
        {
            try
            {
                var res = await _eventService.UpdateEvent(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
