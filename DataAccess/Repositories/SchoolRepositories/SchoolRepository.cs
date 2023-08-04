using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.SchoolDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.SchoolRepositories
{
    public class SchoolRepository : GenericRepository<School>, ISchoolRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public SchoolRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

       public async Task<IEnumerable<School>> GetSchoolByName(string schoolname)
        {
            var schoolList = await _dbContext.Schools.Where(x => x.Name.ToLower().Contains(schoolname.ToLower())).ToListAsync();
            return schoolList;
        }

        public async Task<List<GetSchoolDto>> GetSchoolByEventId(Guid eventid)
        {
            var schoolList = await _dbContext.SchoolEvents.Include(se => se.School).Where(se => se.EventId.Equals(eventid)).Select(s => new GetSchoolDto
            {
                Id = s.School.Id,
                Name = s.School.Name,
                PhoneNumber = s.School.PhoneNumber,
                Email = s.School.Email,
                Address = s.School.Address
            }).ToListAsync();
            return schoolList;
        }
    }
}
