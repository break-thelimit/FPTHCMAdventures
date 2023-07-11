using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.UserDto;
using DataAccess.Repositories.SchoolRepositories;
using DataAccess.Repositories.TaskRepositories;
using DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public UserService(IUserRepository userRepository, IMapper mapper,ISchoolRepository schoolRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _schoolRepository = schoolRepository;   
        }
        public async Task<ServiceResponse<Guid>> CreateNewUser(CreateUserDto createUserDto)
        {
            var mapper = config.CreateMapper();
            var taskcreate = mapper.Map<User>(createUserDto);
            taskcreate.Id = Guid.NewGuid();
            await _userRepository.AddAsync(taskcreate);

            return new ServiceResponse<Guid>
            {
                Data = taskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }



        public async Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUser()
        {
            var eventList = await _userRepository.GetAllAsync<GetUserDto>();

            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<GetUserDto>>
                {
                    Data = eventList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetUserDto>>
                {
                    Data = eventList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<UserDto>> GetUserByEmail(string Email)
        {
            try
            {
                List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
                {
                    x => x.Players

                };
                var taskDetail = await _userRepository.GetByWithCondition(x => x.Email == Email, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<UserDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<UserDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<UserDto>
                {
                    Data = taskDetailDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<UserDto>> GetUserById(Guid userId)
        {
            try
            {
                List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
                {
                    x => x.Players
                  
                };
                var taskDetail = await _userRepository.GetByWithCondition(x => x.Id == userId, includes, true);
                var _mapper = config.CreateMapper();
                var taskDetailDto = _mapper.Map<UserDto>(taskDetail);
                if (taskDetail == null)
                {

                    return new ServiceResponse<UserDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<UserDto>
                {
                    Data = taskDetailDto,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
            {
                return new ServiceResponse<string>
                {
                    Message = "Invalid Record Id",
                    Success = false,
                    StatusCode = 500
                };

            }
            var checkTask = await _userRepository.GetAsync(id);
            if (checkTask == null)
            {
                return new ServiceResponse<string>
                {
                    Message = "Task null",
                    Success = false,
                    StatusCode = 500
                };
            }
            else
            {
                _mapper.Map(updateUserDto, checkTask);

                try
                {
                    await _userRepository.UpdateAsync(checkTask);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CountryExists(id))
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "No rows",
                            Success = false,
                            StatusCode = 500
                        };
                    }
                    else
                    {
                        throw;
                    }
                }
                return new ServiceResponse<string>
                {
                    Message = "Update Success",
                    Success = true,
                    StatusCode = 500
                };
            }
        }
        private async Task<bool> CountryExists(Guid id)
        {
            return await _userRepository.Exists(id);
        }

        public async Task<ServiceResponse<IEnumerable<GetUserListWithSchoolNameDto>>> GetUserWithSchoolName()
        {
            var users = await _userRepository.GetAllAsync<GetUserDto>();
            var userList = new List<GetUserListWithSchoolNameDto>();

            if (userList != null)
            {
                foreach (var user in users)
                {
                    if(user.SchoolId != null)
                    {
                        var school = await _schoolRepository.GetAsync<SchoolDto>(user.SchoolId);
                        if (school != null)
                        {
                            if (user != null)
                            {
                                var playerData = new GetUserListWithSchoolNameDto
                                {
                                    Id = user.Id,
                                    FullName = user.FullName,
                                    Email = user.Email,
                                    SchoolName = school.SchoolName,
                                    Gender = user.Gender,
                                    PhoneNumber = user.PhoneNumber,
                                    Status = user.Status

                                };
                                userList.Add(playerData);

                            }
                        }
                        else
                        {
                            var playerData = new GetUserListWithSchoolNameDto
                            {
                                Id = user.Id,
                                FullName = user.FullName,
                                Email = user.Email,
                                Gender = user.Gender,
                                PhoneNumber = user.PhoneNumber,
                                Status = user.Status

                            };
                            userList.Add(playerData);
                        }
                    }
                    else
                    {
                        var playerData = new GetUserListWithSchoolNameDto
                        {
                            Id = user.Id,
                            FullName = user.FullName,
                            Email = user.Email,
                            Gender = user.Gender,
                            PhoneNumber = user.PhoneNumber,
                            Status = user.Status

                        };
                        userList.Add(playerData);
                    }
                }
                return new ServiceResponse<IEnumerable<GetUserListWithSchoolNameDto>>
                {
                    Data = userList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetUserListWithSchoolNameDto>>
                {
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }
    }
}

