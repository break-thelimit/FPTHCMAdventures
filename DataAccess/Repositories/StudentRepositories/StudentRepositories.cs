﻿using AutoMapper;
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
        private readonly db_a9c31b_capstoneContext _dbContext;
        private readonly IMapper _mapper;

        public StudentRepositories(db_a9c31b_capstoneContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<string>> GetExistingEmails()
        {
            return await _dbContext.Students.Select(s => s.Email).ToListAsync();
        }

        public async Task<List<string>> GetExistingPhoneNumbers()
        {
            return await _dbContext.Students.Select(s => s.Phonenumber.ToString()).ToListAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetStudentBySchoolId(Guid SchoolId)
        {
            var students = await _dbContext.Students.Include(x => x.School).Where(s => s.SchoolId.Equals(SchoolId)).Select(x=>new StudentDto
            {
                Id = x.Id,
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

        public async Task<IEnumerable<GetStudentBySchoolAndEvent>> GetStudentBySchoolIdAndEventId(Guid SchoolId, Guid eventId)
        {
            var eventName = await _dbContext.Events.Where(x => x.Id == eventId).FirstOrDefaultAsync();
            if(eventName != null)
            {
                var school = await _dbContext.SchoolEvents.Include(x => x.School).Where(x => x.SchoolId == SchoolId && x.EventId == eventId).FirstOrDefaultAsync();
                if (school != null)
                {
                    
                    var students = await _dbContext.Students
                                    .Include(x => x.School).ThenInclude(x => x.SchoolEvents)
                                    .Where(s => s.SchoolId.Equals(school.SchoolId) && s.School.SchoolEvents.Any(se => se.EventId == eventId))
                                    .ToListAsync();
                    var studentDtos = students.Select(x => new GetStudentBySchoolAndEvent
                    {
                        Id = x.Id,
                        EventName = eventName.Name,
                        Passcode = x.Players.FirstOrDefault(player => !string.IsNullOrEmpty(player.Passcode))?.Passcode,
                        Schoolname = x.School.Name,
                        Fullname = x.Fullname,
                        Email = x.Email,
                        Phonenumber = x.Phonenumber,
                        GraduateYear = x.GraduateYear,
                        Classname = x.Classname,
                        Status = x.Status
                    }).ToList();

                    // You may need to add additional logic here to filter students based on the eventId.

                    return studentDtos;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;

            }
        }
        public async Task<IEnumerable<GetStudentWithPlayerDto>> GetStudentsBySchoolIdAsync(Guid schoolId)
        {
            return await _dbContext.Students
                .Where(student => student.SchoolId == schoolId)
                .Select(student => new GetStudentWithPlayerDto
                {
                    // Map properties from Student entity to StudentDto
                    Id = student.Id,
                    Schoolname = student.School.Name,
                    Fullname = student.Fullname,
                    Email = student.Email,
                    Phonenumber = student.Phonenumber,
                    GraduateYear = student.GraduateYear,
                    Classname = student.Classname,
                    Passcode = student.Player.Passcode,
                    Status = student.Status
                })
                .ToListAsync();
        }
    }
}
