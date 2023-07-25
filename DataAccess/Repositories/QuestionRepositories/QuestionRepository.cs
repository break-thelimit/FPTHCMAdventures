using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.QuestionDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.QuestionRepositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<GetQuestionDto>> GetAllQuestionAsync()
        {
            var ranklist1 = await _dbContext.Questions.Include(x => x.Major).Select(x => new GetQuestionDto
            {
                Id = x.Id,
                MajorId=x.MajorId,
                MajorName=x.Major.Name,
                QuestionName=x.QuestionName
            }).ToListAsync();
            return ranklist1;
        }
    }
}
