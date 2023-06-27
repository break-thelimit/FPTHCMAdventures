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
    [Route("api/xa1/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {

        /*IActivityRepository activityRepository = new ActivityRepository();

        private readonly IConfiguration Configuration;

        public ActivitiesController(IConfiguration config)
        {
            Configuration = config;
        }

        // GET: api/xa1/Activities
        [HttpGet]
        public JsonResult GetActivities()
        {
            IEnumerable<Activity> list = activityRepository.GetAllActivities();
            return new JsonResult(new
            {
                result= list
            });
        }

        [HttpGet("forvisitor")]
        public JsonResult GetActivitiesForVisitor()
        {
            IEnumerable<Activity> list = (IQueryable<Activity>)activityRepository.GetActivitiesForVisitors();
            return new JsonResult(new
            {
                result = list
            });
        }

        [HttpGet("forstudent")]
        public JsonResult GetActivitiesForStudent()
        {
            IEnumerable<Activity> list = (IQueryable<Activity>)activityRepository.GetActivitiesForStudents();
            return new JsonResult(new
            {
                result = list
            });
        }
        *//*        // GET: api/Activities/5
                [HttpGet("{id}")]
                public async Task<ActionResult<Activity>> GetActivity(Guid id)
                {
                    var activity = await _context.Activities.FindAsync(id);

                    if (activity == null)
                    {
                        return NotFound();
                    }

                    return activity;
                }*//*

        // PUT: api/Activities/5
        [HttpPut]
        public JsonResult UpdateActivity([FromForm] Activity activity)
        {
            Activity tmp = activityRepository.UpdateActivity(activity);
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

        // POST: api/Activities
        [HttpPost]
        public JsonResult CreateActivity([FromForm] Activity activity, int bonuspoint, Boolean hasInviteCode)
        {
            Activity tmp = activityRepository.CreateActivity(activity,bonuspoint,hasInviteCode);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // created successfully!
                    product_id = tmp.Id,
                    message = "Created successfully!"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = 406, // created failed with invalid input value!
                    message = "This product name has existed in system!"
                });
            }
        }

*//*        private bool ActivityExists(Guid id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }*/
    }
}
