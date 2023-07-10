﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Model;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Service;
using System.Net;
using Service.Services.EventService;
using AutoMapper;
using DataAccess.Dtos.EventDto;
using DataAccess.Exceptions;

namespace XavalorAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IMapper mapper,IEventService eventService)
        {
            this._mapper = mapper;
            _eventService = eventService;
        }


        //Phan cua kiet
        [HttpGet(Name = "GetEventList")]
        
        public async Task<ActionResult<ServiceResponse<GetEventDto>>> GetEventList()
        {
            try
            {
                var res = await _eventService.GetEvent();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEventById(Guid id)
        {
            var eventDetail = await _eventService.GetEventById(id);
            return Ok(eventDetail);
        }

        [HttpPost("event", Name = "CreateNewEvent")]

        public async Task<ActionResult<ServiceResponse<EventDto>>> CreateNewEvent(CreateEventDto eventDto)
        {
            try
            {
                var res = await _eventService.CreateNewEvent(eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        } 
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<EventDto>>> CreateNewEvent(Guid id, [FromBody] UpdateEventDto eventDto)
        {
            try
            {
                var res = await _eventService.UpdateEvent(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("upload-excel-event")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            var serviceResponse = await _eventService.ImportDataFromExcel(file);

            if (serviceResponse.Success)
            {
                // Xử lý thành công
                return Ok(serviceResponse.Message);
            }
            else
            {
                // Xử lý lỗi
                return BadRequest(serviceResponse.Message);
            }
        }

        [HttpGet("excel-template-event")]
        public async Task<IActionResult> DownloadExcelTemplate()
        {
            var serviceResponse = await _eventService.DownloadExcelTemplate();

            if (serviceResponse.Success)
            {
                // Trả về file Excel dưới dạng response
                return new FileContentResult(serviceResponse.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = "SampleDataEvent.xlsx"
                };
            }
            else
            {
                // Xử lý lỗi nếu có
                return BadRequest(serviceResponse.Message);
            }
        }
    }
}
