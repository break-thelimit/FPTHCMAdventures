using AutoMapper;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.EventTaskService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.MajorService;
using DataAccess.Dtos.MajorDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorsController : ControllerBase
    {
        private readonly IMajorService _majorService;
        private readonly IMapper _mapper;

        public MajorsController(IMapper mapper, IMajorService majorService)
        {
            this._mapper = mapper;
            _majorService = majorService;
        }


        [HttpGet(Name = "GetMajorList")]

        public async Task<ActionResult<ServiceResponse<GetEventTaskDto>>> GetEventTaskList()
        {
            try
            {
                var res = await _majorService.GetMajor();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MajorDto>> GetEventTaskById(Guid id)
        {
            var eventDetail = await _majorService.GetEventById(id);
            return Ok(eventDetail);
        }

        [HttpPost("major", Name = "CreateMajor")]

        public async Task<ActionResult<ServiceResponse<MajorDto>>> CreateNewEvent(CreateMajorDto eventTaskDto)
        {
            try
            {
                var res = await _majorService.CreateNewMajor(eventTaskDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<MajorDto>>> CreateNewEvent(Guid id, [FromBody] UpdateMajorDto eventDto)
        {
            try
            {
                var res = await _majorService.UpdateMajor(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
