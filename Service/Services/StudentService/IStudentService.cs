﻿using DataAccess.Dtos.SchoolDto;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dtos.StudentDto;

namespace Service.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<IEnumerable<GetStudentDto>>> GetStudent();
        Task<ServiceResponse<StudentDto>> GetStudentById(Guid studentId);
        Task<ServiceResponse<Guid>> CreateNewStudent(CreateStudentDto createStudentDto);
        Task<ServiceResponse<string>> UpdateStudent(Guid id, UpdateStudentDto studentDto);
        Task<ServiceResponse<PagedResult<StudentDto>>> GetStudentWithPage(QueryParameters queryParameters);
        Task<ServiceResponse<string>> DisableStudent(Guid id);
        Task<ServiceResponse<IEnumerable<StudentDto>>> GetStudentBySchoolId(Guid id);
    }
}