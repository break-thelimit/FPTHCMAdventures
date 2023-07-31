using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.LocationDto;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.StudentDto;
using DataAccess.Repositories.SchoolRepositories;
using DataAccess.Repositories.StudentRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepositories _studentRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public StudentService(IStudentRepositories studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Guid>> CreateNewStudent(CreateStudentDto createStudentDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Student>(createStudentDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _studentRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<string>> DisableStudent(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<IEnumerable<GetStudentDto>>> GetStudent()
        {
            var majorList = await _studentRepository.GetAllAsync<GetStudentDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetStudentDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetStudentDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List student null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<StudentDto>> GetStudentById(Guid studentId)
        {
            try
            {

                var eventDetail = await _studentRepository.GetAsync<StudentDto>(studentId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<StudentDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<StudentDto>
                {
                    Data = eventDetail,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<PagedResult<StudentDto>>> GetStudentWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _studentRepository.GetAllAsync<StudentDto>(queryParameters);
            return new ServiceResponse<PagedResult<StudentDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<string>> UpdateStudent(Guid id, UpdateStudentDto studentDto)
        {
            try
            {

                studentDto.Id = id;
                await _studentRepository.UpdateAsync(id, studentDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentExists(id))
                {
                    return new ServiceResponse<string>
                    {
                        Message = "Invalid Record Id",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ServiceResponse<IEnumerable<StudentDto>>> GetStudentBySchoolId(Guid id)
        {
            var studentList = await _studentRepository.GetStudentBySchoolId(id);

            if (studentList != null)
            {
                return new ServiceResponse<IEnumerable<StudentDto>>
                {
                    Data = studentList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<StudentDto>>
                {
                    Data = studentList,
                    Success = false,
                    Message = "Faile because List student null",
                    StatusCode = 200
                };
            }
        }
        private async Task<bool> StudentExists(Guid id)
        {
            return await _studentRepository.Exists(id);
        }
        public byte[] GenerateExcelTemplate()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SampleDataStudent");

                // Thiết lập header cho các cột
                worksheet.Cells[1, 1].Value = "School Name";
                worksheet.Cells[1, 2].Value = "Full Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Phone Number";
                worksheet.Cells[1, 5].Value = "GraduateYear";
                worksheet.Cells[1, 6].Value = "Class Name";
                worksheet.Cells[1, 7].Value = "Status";

                // Lưu file Excel vào MemoryStream
                var stream = new MemoryStream(package.GetAsByteArray());
                return stream.ToArray();
            }
        }
        public async Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "No file uploaded.",
                    Success = false,
                    StatusCode = 400
                };
            }

            if (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "Invalid file format. Only Excel files are allowed.",
                    Success = false,
                    StatusCode = 400
                };
            }
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet != null)
                        {
                            var rowCount = worksheet.Dimension.Rows;
                            var dataList = new List<StudentDto>();
                            var errorMessages = new List<string>();

                            for (int row = 2; row <= rowCount - 1; row++)
                            {
                                var data = new StudentDto
                                {
                                    Schoolname = worksheet.Cells[row, 1].Text,
                                    Fullname = worksheet.Cells[row, 2].Text,
                                    Email    = worksheet.Cells[row, 3].Text,
                                    Phonenumber = long.Parse(worksheet.Cells[row, 4].Text),
                                    GraduateYear = worksheet.Cells[row, 5].Text,
                                    Classname = worksheet.Cells[row, 6].Text,
                                    Status = worksheet.Cells[row, 7].Text
                                };
                                if (string.IsNullOrEmpty(data.Schoolname))
                                {
                                    errorMessages.Add($"Row {row}: Schoolname is required.");
                                }

                                if (string.IsNullOrEmpty(data.Fullname))
                                {
                                    errorMessages.Add($"Row {row}: Fullname is required.");
                                }

                                if (string.IsNullOrEmpty(data.Email) || !IsValidEmail(data.Email))
                                {
                                    errorMessages.Add($"Row {row}: Invalid email format.");
                                }

                                if (data.Phonenumber <= 0)
                                {
                                    errorMessages.Add($"Row {row}: Invalid phone number.");
                                }
                                dataList.Add(data);
                            }
                            if (errorMessages.Any())
                            {
                                return new ServiceResponse<string>
                                {
                                    Data = null,
                                    Message = string.Join("\n", errorMessages),
                                    Success = false,
                                    StatusCode = 400
                                };
                            }
                            // Start from row 2 to skip the header row

                            var mapper = config.CreateMapper();
                            var locations = _mapper.Map<List<Student>>(dataList);
                            await _studentRepository.AddRangeAsync(locations);
                            await _studentRepository.SaveChangesAsync();

                        }
                    }
                }
                return new ServiceResponse<string>
                {
                    Data = "Upload successful.",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Data = ex.Message,
                    Message = "Failed to process uploaded file.",
                    Success = false,
                    StatusCode = 500
                };
            }
        }
        private bool IsValidEmail(string email)
        {
            // Implement email validation logic based on your requirements
            // You can use regular expressions or other methods to validate the email format
            // For simplicity, we will just check if the email contains "@" symbol.
            return email.Contains("@");
        }

        public async Task<ServiceResponse<byte[]>> DownloadExcelTemplate()
        {
            byte[] fileContents;
            try
            {
                fileContents = GenerateExcelTemplate();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new ServiceResponse<byte[]>
                {
                    Data = null,
                    Message = "Failed to generate Excel template.",
                    Success = false,
                    StatusCode = 500
                };
            }

            // Trả về file Excel dưới dạng byte array
            return new ServiceResponse<byte[]>
            {
                Data = fileContents,
                Success = true,
                StatusCode = 200
            };
        }
    }
}
