using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.StudentDto;
using DataAccess.Repositories.SchoolRepositories;
using DataAccess.Repositories.StudentRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepositories _studentRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public StudentService(IStudentRepositories studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Guid>> CreateNewStudent(CreateStudentDto createStudentDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Student>(createStudentDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _studentRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<string>> DisableStudent(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<IEnumerable<GetStudentDto>>> GetStudent()
        {
            var majorList = await _studentRepository.GetAllAsync<GetStudentDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetStudentDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetStudentDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<StudentDto>> GetStudentById(Guid studentId)
        {
            try
            {

                var eventDetail = await _studentRepository.GetAsync<StudentDto>(studentId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<StudentDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<StudentDto>
                {
                    Data = eventDetail,
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

        public async Task<ServiceResponse<PagedResult<StudentDto>>> GetStudentWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _studentRepository.GetAllAsync<StudentDto>(queryParameters);
            return new ServiceResponse<PagedResult<StudentDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<string>> UpdateStudent(Guid id, UpdateStudentDto studentDto)
        {
            try
            {

                studentDto.Id = id;
                await _studentRepository.UpdateAsync(id, studentDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentExists(id))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Invalid Record Id",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    throw;
                }
            }
        }
        private async Task<bool> StudentExists(Guid id)
        {
            return await _studentRepository.Exists(id);
        }
    }
}
