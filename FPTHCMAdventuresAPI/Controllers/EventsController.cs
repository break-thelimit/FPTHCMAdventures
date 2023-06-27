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

namespace XavalorAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        IEventRepository eventRepository = new EventRepository();
        private readonly IConfiguration Configuration;
        public EventsController(IConfiguration config)
        {
            Configuration = config;
        }

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

/*        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }*/

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
    }
}
