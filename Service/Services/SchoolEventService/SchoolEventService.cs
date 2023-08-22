using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.SchoolEventDto;
using DataAccess.Repositories.SchoolEventRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.SchoolEventService
{
    public class SchoolEventService : ISchoolEventService
    {
        private readonly ISchoolEventRepository _schoolEventRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public SchoolEventService(ISchoolEventRepository roleRepository, IMapper mapper)
        {
            _schoolEventRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewSchoolEvent(CreateSchoolEventDto createSchoolEventDto)
        {
            if (await _schoolEventRepository.ExistsAsync(se => se.EventId == createSchoolEventDto.EventId && se.SchoolId == createSchoolEventDto.SchoolId))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: School Event with the same EventId and SchoolId already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            createSchoolEventDto.InvitationLetter = createSchoolEventDto.InvitationLetter.Trim();
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<SchoolEvent>(createSchoolEventDto);
            eventTaskcreate.Id = Guid.NewGuid();

            await _schoolEventRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<SchoolEventDto>>> GetSchoolEvent()
        {
            var majorList = await _schoolEventRepository.GetAllAsync<SchoolEventDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<SchoolEventDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<SchoolEventDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<SchoolEventDto>> GetSchoolEventById(Guid eventId)
        {
            try
            {

                var eventDetail = await _schoolEventRepository.GetAsync<SchoolEventDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<SchoolEventDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<SchoolEventDto>
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

        public async Task<ServiceResponse<bool>> UpdateSchoolEvent(Guid id, UpdateSchoolEventDto schoolEventDto)
        {
            if (await _schoolEventRepository.ExistsAsync(se => se.EventId == schoolEventDto.EventId && se.SchoolId == schoolEventDto.SchoolId && se.Id != id))
            {
                return new ServiceResponse<bool>
                {
                    Message = "Duplicated data: School Event with the same EventId and SchoolId already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }

            try
            {
                schoolEventDto.InvitationLetter = schoolEventDto.InvitationLetter.Trim();

                await _schoolEventRepository.UpdateAsync(id, schoolEventDto);
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
            return await _schoolEventRepository.Exists(id);
        }
        public async Task<ServiceResponse<List<GetSchoolDto>>> GetSchoolByEventId(Guid eventid)
        {
            var schoolList = await _schoolEventRepository.GetSchoolByEventId(eventid);

            if (schoolList != null)
            {
                return new ServiceResponse<List<GetSchoolDto>>
                {
                    Data = schoolList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<List<GetSchoolDto>>
                {
                    Data = schoolList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }
    }
}
