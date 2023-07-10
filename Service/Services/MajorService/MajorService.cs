using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.MajorDto;
using DataAccess.Repositories.EventTaskRepositories;
using DataAccess.Repositories.MajorRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.MajorService
{
    public class MajorService : IMajorService
    {
        private readonly IMajorRepository _majorRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public MajorService(IMajorRepository majorRepository, IMapper mapper)
        {
            _majorRepository = majorRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewMajor(CreateMajorDto createMajorDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Major>(createMajorDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _majorRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<MajorDto>> GetEventById(Guid eventId)
        {
            try
            {

                var eventDetail = await _majorRepository.GetAsync<MajorDto>(eventId);
               
                if (eventDetail == null)
                {

                    return new ServiceResponse<MajorDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<MajorDto>
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

        public async Task<ServiceResponse<IEnumerable<GetMajorDto>>> GetMajor()
        {
            var majorList = await _majorRepository.GetAllAsync<GetMajorDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetMajorDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetMajorDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<string>> UpdateMajor(Guid id, UpdateMajorDto majorDto)
        {
            try
            {
                await _majorRepository.UpdateAsync(id, majorDto);
                return new ServiceResponse<string>
                {
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventTaskExists(id))
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
        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _majorRepository.Exists(id);
        }
    }
}
