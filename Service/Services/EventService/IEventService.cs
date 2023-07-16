﻿using DataAccess;
using DataAccess.Dtos.EventDto;
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
        Task<ServiceResponse<Guid>> CreateNewEvent(CreateEventDto createEventDto);
        Task<ServiceResponse<string>> UpdateEvent(Guid id,UpdateEventDto eventDto);

        Task<ServiceResponse<byte[]>> DownloadExcelTemplate();
        Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file);
        Task<ServiceResponse<PagedResult<EventDto>>> GetEventWithPage(QueryParameters queryParameters);
    }
}
