<<<<<<< HEAD
﻿using AutoMapper;
using DataAccess.Dtos.TaskItemDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.TaskItemService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.UserService;
using DataAccess.Dtos.UserDto;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Model;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
>>>>>>> main

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
<<<<<<< HEAD
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
=======
        IUserRepository userRepository = new UserRepository();
        private readonly IConfiguration Configuration;

        public UsersController(IConfiguration config)
        {
            Configuration = config;
        }

        // GET: api/Users
        [HttpGet]
        public JsonResult GetUsers()
        {
            IEnumerable<User> list = userRepository.GetUsers();
            return new JsonResult(new
            {
                result = list
            });
        }

        // GET: api/Users/5
        [HttpGet("getuser/{userid}")]
        public JsonResult GetUser(Guid id)
        {
            User user = userRepository.GetUserById(id);
            return new JsonResult(new
            {
                result = user
            });
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public JsonResult PutUser([FromForm] User user)
        {
           User tmp = userRepository.UpdateUser(user);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // updated successfully!
                    user_id = tmp.Id,
                    message = "Updated successfully!"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = 406, // updated failed with invalid input value!
                    message = "This user hasn't existed in system!"
                });
            }
        }

        // POST: api/Users
        [HttpPost("register")]
        public JsonResult Register(string role, [FromBody] User user)
        {
            bool check = userRepository.CreateAccount(role,user);
            if (check)
            {
                return new JsonResult(new
                {
                    status = 200
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = 401,
                    message = "Sorry, this email has existed in our system!"
                });
            }
        }
    }
}
>>>>>>> main
