using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.QuestionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<ServiceResponse<IEnumerable<GetQuestionDto>>> GetQuestion();
        Task<ServiceResponse<QuestionDto>> GetQuestionById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewQuestion(CreateQuestionDto createQuestionDto);
        Task<ServiceResponse<string>> UpdateQuestion(Guid id, UpdateQuestionDto questionDto);
    }
}
