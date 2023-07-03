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
using AutoMapper;
using Service.Services.EventService;
using Service.Services.TaskService;
using DataAccess.Dtos.EventDto;
using Service;
using DataAccess.Dtos.TaskDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        ITaskRepository taskRepository = new TaskRepository();
        private readonly IConfiguration Configuration;
        public TasksController(IConfiguration config,ITaskService taskService,IMapper mapper)
        {
            Configuration = config;
            _taskService = taskService;
            _mapper = mapper;
        }

        // GET: api/Tasks
/*        [HttpGet]
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
*/
        [HttpGet(Name = "GetTaskList")]

        public async Task<ActionResult<ServiceResponse<GetTaskDto>>> GetEventList()
        {
            try
            {
                var res = await _taskService.GetTask();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("task", Name = "CreateNewTask")]

        public async Task<ActionResult<ServiceResponse<TaskDto>>> CreateNewEvent(CreateTaskDto taskDto)
        {
            try
            {
                var res = await _taskService.CreateNewTask(taskDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<TaskDto>>> CreateNewEvent(Guid id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            try
            {
                var res = await _taskService.UpdateTask(id, updateTaskDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
