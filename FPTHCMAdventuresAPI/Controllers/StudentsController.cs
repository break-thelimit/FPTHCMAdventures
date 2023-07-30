using AutoMapper;
using DataAccess.Dtos.SchoolDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Services.SchoolService;
using Service.Services.StudentService;
using System.Threading.Tasks;
using System;
using DataAccess.Dtos.StudentDto;
using DataAccess;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IMapper mapper, IStudentService studentService)
        {
            this._mapper = mapper;
            _studentService = studentService;
        }
        [HttpGet(Name = "GetStudent")]

        public async Task<ActionResult<ServiceResponse<StudentDto>>> GetStudentList()
        {
            try
            {
                var res = await _studentService.GetStudent();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("student/pagination", Name = "GetStudentListWithPagination")]

        public async Task<ActionResult<ServiceResponse<StudentDto>>> GetStudentListWithPage([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var pagedsResult = await _studentService.GetStudentWithPage(queryParameters);
                return Ok(pagedsResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(Guid id)
        {
            var eventDetail = await _studentService.GetStudentById(id);
            return Ok(eventDetail);
        }

        [HttpPost("student", Name = "CreateNewStudent")]

        public async Task<ActionResult<ServiceResponse<StudentDto>>> CreateNewStudent(CreateStudentDto studentDto)
        {
            try
            {
                var res = await _studentService.CreateNewStudent(studentDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<SchoolDto>>> UpdateStudent(Guid id, [FromBody] UpdateStudentDto studentDto)
        {
            try
            {
                var res = await _studentService.UpdateStudent(id, studentDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
