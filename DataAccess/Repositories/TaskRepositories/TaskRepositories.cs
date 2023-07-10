using AutoMapper;
using BusinessObjects.Model;
using DataAccess.GenericRepositories;
using DataAccess.Repositories.EventRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BusinessObjects.Model.Task;

namespace DataAccess.Repositories.TaskRepositories
{
    public class TaskRepositories : GenericRepository<Task>, ITaskRepositories
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public TaskRepositories(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
