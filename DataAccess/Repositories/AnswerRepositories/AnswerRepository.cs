using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.AnswerDto;
using DataAccess.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.AnswerRepositories
{
    public class AnswerRepository : GenericRepository<Answer> , IAnswerRepository
    {
        private readonly FPTHCMAdventuresDBContext _dbContext;
        private readonly IMapper _mapper;

        public AnswerRepository(FPTHCMAdventuresDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<GetAnswerDto>> GetAllAnswerkAsync()
        {
            var ranklist1 = await _dbContext.Answers.Include(l => l.Question).Select(x => new GetAnswerDto
            {
                Id = x.Id,
                QuestionId = x.QuestionId,
                QuestionName = x.Question.QuestionName,
                Answer=x.Answer1,
                IsRight=x.IsRight
            }).ToListAsync();
            return ranklist1;
        }
    }
}
