using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.StudentDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories.StudentRepositories
{
    public class StudentRepositories : GenericRepository<Student>, IStudentRepositories
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public StudentRepositories(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentBySchoolId(Guid SchoolId)
        {
            var students = await _dbContext.Students.Where(s => s.SchoolId.Equals(SchoolId)).Include(x=>x.School).Select(x=>new StudentDto
            {
                Schoolname=x.School.Name,
                Fullname=x.Fullname,
                Email=x.Email,
                Phonenumber=x.Phonenumber,
                GraduateYear=x.GraduateYear,
                Classname=x.Classname,
                Status=x.Status
            }).ToListAsync();
            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);
            return studentDtos;
        }
    }
}
