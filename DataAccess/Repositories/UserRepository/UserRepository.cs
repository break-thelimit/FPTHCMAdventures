using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.UserDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> GetUserIdByUserName(string userName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userName);
            return user.Id;
        }

        public async Task<List<UserDto>> GetAllUserAsync()
        {
            var userlist1 = await _dbContext.Users.Include(x=>x.School).Include(r=>r.Role).Select(x => new UserDto
            {
                Id=x.Id,
                SchoolId= x.SchoolId,
                SchoolName = x.School.Name,
                RoleId=x.RoleId,
                RoleName=x.Role.RoleName,
                Fullname= x.Fullname,
                Email= x.Email,
                Username = x.Username,
                Password= x.Password,
                PhoneNumber= x.PhoneNumber,
                Gender= x.Gender,
                Status=x.Status
            }).ToListAsync();
            return userlist1;
        }
    }
}

