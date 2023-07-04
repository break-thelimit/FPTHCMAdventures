using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.RankDto;
using DataAccess.Repositories.PlayerRepositories;
using DataAccess.Repositories.RankRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RankService
{
    public class RankService : IRankService
    {
        private readonly IRankRepository _rankRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public RankService(IRankRepository rankRepository, IMapper mapper)
        {
            _rankRepository = rankRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewRank(CreateRankDto createRankDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Rank>(createRankDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _rankRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetRankDto>>> GetRank()
        {
            var majorList = await _rankRepository.GetAllAsync<GetRankDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetRankDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetRankDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<RankDto>> GetRankById(Guid eventId)
        {
            try
            {

                var eventDetail = await _rankRepository.GetAsync<RankDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<RankDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<RankDto>
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

        public async Task<ServiceResponse<string>> UpdateRank(Guid id, UpdateRankDto rankDto)
        {
            try
            {
                await _rankRepository.UpdateAsync(id, rankDto);
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
            return await _rankRepository.Exists(id);
        }
       
    }
}
