using BusinessObjects.Model;
using DataAccess.Dtos.StudentDto;
using DataAccess.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.StudentRepositories
{
    public interface IStudentRepositories : IGenericRepository<Student>
    {
        Task<IEnumerable<StudentDto>> GetStudentBySchoolId(Guid SchoolId);
    }
}

