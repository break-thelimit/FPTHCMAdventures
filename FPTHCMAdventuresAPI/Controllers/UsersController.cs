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
    public class UsersController : ControllerBase
    {
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
