﻿using BusinessObjects.Model;
using DataAccess.Dtos.StudentDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.StudentRepositories
{
    public interface IStudentRepositories : IGenericRepository<Student>
    {
        Task<IEnumerable<StudentDto>> GetStudentBySchoolId(Guid SchoolId);
        Task<IEnumerable<GetStudentBySchoolAndEvent>> GetStudentBySchoolIdAndEventId(Guid SchoolId,Guid eventId);
        Task<List<string>> GetExistingEmails();
        Task<List<string>> GetExistingPhoneNumbers();
        Task<IEnumerable<GetStudentWithPlayerDto>> GetStudentsBySchoolIdAsync(Guid schoolId);
    }
}

