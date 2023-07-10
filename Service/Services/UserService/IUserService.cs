using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUser();
        Task<ServiceResponse<UserDto>> GetUserById(Guid userId);
        Task<ServiceResponse<UserDto>> GetUserByEmail(string Email);

        Task<ServiceResponse<Guid>> CreateNewUser(CreateUserDto createUserDto);
        Task<ServiceResponse<string>> UpdateUser(Guid id, UpdateUserDto updateUserDto);
    }
}
