using AutoMapper;
using BusinessObjects.Model;
using DataAccess.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.SchoolEventRepositories
{
    public class SchoolEventRepository : GenericRepository<SchoolEvent>,ISchoolEventRepository 
    {
        private readonly db_a9c31b_capstoneContext _dbContext;
        private readonly IMapper _mapper;

        public SchoolEventRepository(db_a9c31b_capstoneContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
