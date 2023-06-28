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

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        ITaskRepository taskRepository = new TaskRepository();
        private readonly IConfiguration Configuration;
        public TasksController(IConfiguration config)
        {
            Configuration = config;
        }

        // GET: api/Tasks
        [HttpGet]
        public JsonResult GetTasks()
        {
            IEnumerable<BusinessObjects.Model.Task> list = taskRepository.GetTasks();
            return new JsonResult(new
            {
                result = list
            });
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public JsonResult GetTask(Guid id)
        {
            BusinessObjects.Model.Task task = taskRepository.GetTaskByID(id);
            return new JsonResult(new
            {
                result = task
            });
        }


        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public JsonResult UpdateTask([FromForm] BusinessObjects.Model.Task task)
        {
            BusinessObjects.Model.Task tmp = taskRepository.CreateTask(task);
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
                    message = "This task hasn't existed in system!"
                });
            }
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public JsonResult CreateTask([FromForm] BusinessObjects.Model.Task task)
        {
            BusinessObjects.Model.Task tmp = taskRepository.CreateTask(task);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // created successfully!
                    task_id = tmp.Id,
                    message = "Created successfully!"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = 406, // created failed with invalid input value!
                    message = "This task has existed in system!"
                });
            }
        }
    }
}
