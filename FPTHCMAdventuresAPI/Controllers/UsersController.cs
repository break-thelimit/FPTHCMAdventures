using AutoMapper;
using DataAccess.Dtos.TaskItemDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.TaskItemService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.UserService;
using DataAccess.Dtos.UserDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserService userService)
        {
            this._mapper = mapper;
            _userService = userService;
        }


        [HttpGet(Name = "GetUserList")]

        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetTaskItemList()
        {
            try
            {
                var res = await _userService.GetUser();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var eventDetail = await _userService.GetUserById(id);
            return Ok(eventDetail);
        }
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var eventDetail = await _userService.GetUserByEmail(email);
            return Ok(eventDetail);
        }

        [HttpPost("user", Name = "CreateNewUser")]

        public async Task<ActionResult<ServiceResponse<UserDto>>> CreateNewTaskItem(CreateUserDto answerDto)
        {
            try
            {
                var res = await _userService.CreateNewUser(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<UserDto>>> UpdateTaskItem(Guid id, [FromBody] UpdateUserDto eventDto)
        {
            try
            {
                var res = await _userService.UpdateUser(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}