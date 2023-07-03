using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.EventTaskService
{
    public interface IEventTaskService
    {
        Task<ServiceResponse<IEnumerable<GetEventTaskDto>>> GetEventTask();
        Task<ServiceResponse<EventTaskDto>> GetEventById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewEventTask(CreateEventTaskDto createEventTaskDto);
        Task<ServiceResponse<string>> UpdateTaskEvent(Guid id, UpdateEventTaskDto eventTaskDto);
    }
}
