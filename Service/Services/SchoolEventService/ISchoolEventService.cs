﻿using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.SchoolEventDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.SchoolEventService
{
    public interface ISchoolEventService
    {
        Task<ServiceResponse<IEnumerable<SchoolEventDto>>> GetSchoolEvent();
        Task<ServiceResponse<SchoolEventDto>> GetSchoolEventById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewSchoolEvent(CreateSchoolEventDto createSchoolEventDto);
        Task<ServiceResponse<bool>> UpdateSchoolEvent(Guid id, UpdateSchoolEventDto schoolEventDto);

        Task<ServiceResponse<List<GetSchoolDto>>> GetSchoolByEventId(Guid eventid);

    }
}
