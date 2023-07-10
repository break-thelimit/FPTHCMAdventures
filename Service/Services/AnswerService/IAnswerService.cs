using DataAccess.Dtos.AnswerDto;
using DataAccess.Dtos.EventDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AnswerService
{
    public interface IAnswerService
    {
        Task<ServiceResponse<IEnumerable<GetAnswerDto>>> GetAnswer();
        Task<ServiceResponse<AnswerDto>> GetAnswerById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewAnswer(CreateAnswerDto createAnswerDto);
        Task<ServiceResponse<string>> UpdateAnswer(Guid id, UpdateAnswerDto eventDto);
    }
}
