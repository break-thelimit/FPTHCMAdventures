using AutoMapper;
using DataAccess.Dtos.RoleDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.RoleService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.TaskItemService;
using DataAccess.Dtos.TaskItemDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;
        private readonly IMapper _mapper;

        public TaskItemsController(IMapper mapper, ITaskItemService taskItemService)
        {
            this._mapper = mapper;
            _taskItemService = taskItemService;
        }


        [HttpGet(Name = "GetTaskItem")]

        public async Task<ActionResult<ServiceResponse<GetTaskItemDto>>> GetTaskItemList()
        {
            try
            {
                var res = await _taskItemService.GetTaskItem();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskItemById(Guid id)
        {
            var eventDetail = await _taskItemService.GetTaskItemById(id);
            return Ok(eventDetail);
        }

        [HttpPost("taskitem", Name = "CreateNewTaskItem")]

        public async Task<ActionResult<ServiceResponse<TaskItemDto>>> CreateNewTaskItem(CreateTaskItemDto answerDto)
        {
            try
            {
                var res = await _taskItemService.CreateNewTaskItem(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<TaskItemDto>>> UpdateTaskItem(Guid id, [FromBody] UpdateTaskItemDto eventDto)
        {
            try
            {
                var res = await _taskItemService.UpdateTaskItem(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
