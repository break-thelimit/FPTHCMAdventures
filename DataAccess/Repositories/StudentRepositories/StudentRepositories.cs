using AutoMapper;
using BusinessObjects.Model;
using DataAccess.GenericRepositories;


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
    }
}
