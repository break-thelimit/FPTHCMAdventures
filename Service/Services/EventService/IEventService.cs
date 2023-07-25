using DataAccess;
using DataAccess.Dtos.EventDto;
<<<<<<< HEAD
using DataAccess.Dtos.TaskDto;
=======
>>>>>>> origin/main
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.EventService
{
    public interface IEventService 
    {
        Task<ServiceResponse<IEnumerable<GetEventDto>>> GetEvent();
        Task<ServiceResponse<EventDto>> GetEventById(Guid eventId);
        Task<ServiceResponse<IEnumerable<GetEventDto>>> GetEventByDate(DateTime dateTimeStart);        
        Task<ServiceResponse<Guid>> CreateNewEvent(CreateEventDto createEventDto);
        Task<ServiceResponse<string>> UpdateEvent(Guid id,UpdateEventDto eventDto);
        Task<ServiceResponse<IEnumerable<GetTaskAndEventDto>>> GetTaskAndEventListByTimeNow();
        Task<ServiceResponse<byte[]>> DownloadExcelTemplate();
        Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file);
        Task<ServiceResponse<PagedResult<EventDto>>> GetEventWithPage(QueryParameters queryParameters);
<<<<<<< HEAD
    
=======
>>>>>>> origin/main
    }
}
