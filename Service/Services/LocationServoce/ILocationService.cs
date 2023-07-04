﻿using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.LocationDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.LocationServoce
{
    public interface ILocationService
    {
        Task<ServiceResponse<IEnumerable<GetLocationDto>>> GetLocation();
        Task<ServiceResponse<LocationDto>> GetLocationById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewLocation(CreateLocationDto createEventTaskDto);
        Task<ServiceResponse<string>> UpdateLocation(Guid id, UpdateLocationDto eventTaskDto);
        Task<ServiceResponse<byte[]>> DownloadExcelTemplate();
        Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file);    
    }
}
