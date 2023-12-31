﻿using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.QuestionDto;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.SchoolEventDto;
using DataAccess.Repositories.SchoolEventRepositories;
using DataAccess.Repositories.SchoolRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Services.SchoolService
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public SchoolService(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewSchool(CreateSchoolDto createSchoolDto)
        {
            // Kiểm tra xem dữ liệu đã tồn tại trong cơ sở dữ liệu hay chưa
            if (await _schoolRepository.ExistsAsync(s => s.Name == createSchoolDto.Name))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: School with the same name already exists.",
                    Success = false,
                    StatusCode = 400
                };
            } 
            if (await _schoolRepository.ExistsAsync(s => s.Address == createSchoolDto.Address))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: School with the same address already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            createSchoolDto.Address = createSchoolDto.Address.Trim();
            createSchoolDto.Name = createSchoolDto.Name.Trim();
            createSchoolDto.Email = createSchoolDto.Email.Trim();

            // Tiếp tục tiến hành tạo mới dữ liệu
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<School>(createSchoolDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _schoolRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }


        public async Task<ServiceResponse<IEnumerable<GetSchoolDto>>> GetSchool()
        {
            var majorList = await _schoolRepository.GetAllAsync<GetSchoolDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetSchoolDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetSchoolDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<SchoolDto>> GetSchoolById(Guid eventId)
        {
            try
            {

                var eventDetail = await _schoolRepository.GetAsync<SchoolDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<SchoolDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<SchoolDto>
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
        public async Task<ServiceResponse<PagedResult<SchoolDto>>> GetSchoolWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _schoolRepository.GetAllAsync<SchoolDto>(queryParameters);
            return new ServiceResponse<PagedResult<SchoolDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<bool>> UpdateSchool(Guid id, UpdateSchoolDto schoolDto)
        {
            // Kiểm tra xem dữ liệu đã tồn tại trong cơ sở dữ liệu hay chưa
            var existingSchoolWithSameName = await _schoolRepository.ExistsAsync(s => s.Name == schoolDto.Name && s.Id != id);
            var existingSchoolWithSameEmail = await _schoolRepository.ExistsAsync(s => s.Email == schoolDto.Email && s.Id != id);

            if (existingSchoolWithSameName)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Duplicated data: School with the same name already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            if (existingSchoolWithSameEmail)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Duplicated data: School with the same email already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }

            try
            {
                schoolDto.Address = schoolDto.Address.Trim();
                schoolDto.Name = schoolDto.Name.Trim();
                schoolDto.Email = schoolDto.Email.Trim();
                schoolDto.Status = schoolDto.Status.Trim();
                await _schoolRepository.UpdateAsync(id, schoolDto);
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventTaskExists(id))
                {
                    return new ServiceResponse<bool>
                    {
                        Data = false,
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


        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _schoolRepository.Exists(id);
        }
        public async Task<ServiceResponse<bool>> DisableSchool(Guid id)
        {
            var checkEvent = await _schoolRepository.GetAsync<SchoolDto>(id);

            if (checkEvent == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Failed",
                    StatusCode = 400,
                    Success = false
                };
            }
            else
            {
                checkEvent.Status = "INACTIVE";

                await _schoolRepository.UpdateAsync(id, checkEvent);
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }

       

        public async Task<ServiceResponse<IEnumerable<School>>> GetSchoolByName(string schoolname)
        {
            var schoolList = await _schoolRepository.GetSchoolByName(schoolname.Trim());

            if (schoolList != null)
            {
                return new ServiceResponse<IEnumerable<School>>
                {
                    Data = schoolList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<School>>
                {
                    Data = schoolList,
                    Success = false,
                    Message = "Faile because List school null",
                    StatusCode = 200
                };
            }
        }
    }
}
