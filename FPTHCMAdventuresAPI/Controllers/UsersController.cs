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
using DataAccess;

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

        public async Task<ActionResult<ServiceResponse<UserDto>>> GetUserList()
        {
            try
            {
                var res = await _userService.GetAllUser();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("user/pagination", Name = "GetUserListWithPagination")]

        public async Task<ActionResult<ServiceResponse<UserDto>>> GetUserListWithPage([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var pagedsResult = await _userService.GetUserWithPage(queryParameters);
                return Ok(pagedsResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("users/listUser-schoolname", Name = "GetUserWithSchoolNames")]

        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUserWithSchoolName()
        {
            try
            {
                var res = await _userService.GetUserWithSchoolName();
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

        public async Task<ActionResult<ServiceResponse<UserDto>>> CreateNewUser(CreateUserDto userDto)
        {
            try
            {
                var res = await _userService.CreateNewUser(userDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<UserDto>>> UpdateUser(Guid id, [FromBody] UpdateUserDto userDto)
        {
            try
            {
                var res = await _userService.UpdateUser(id, userDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
