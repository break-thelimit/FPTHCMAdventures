﻿using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.LocationDto;
using DataAccess.Repositories.ExchangeHistoryRepositories;
using DataAccess.Repositories.LocationRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ExchangeHistoryService
{
    public class ExchangHistoryService : IExchangeHistoryService
    {
        private readonly IExchangeHistoryRepository _exchangeHistoryRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public ExchangHistoryService(IExchangeHistoryRepository exchangeHistoryRepository, IMapper mapper)
        {
            _exchangeHistoryRepository = exchangeHistoryRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewExchangeHistory(CreateExchangeHistoryDto createEventTaskDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<ExchangeHistory>(createEventTaskDto);
            eventTaskcreate.Id = Guid.NewGuid();
            eventTaskcreate.ExchangeDate = DateTime.UtcNow;
            await _exchangeHistoryRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<ExchangeHistoryDto>> GetExchangeByItemName(string itemName)
        {
            var exchange = await _exchangeHistoryRepository.getExchangeByItemName(itemName);
           
            if(exchange != null)
            {
                return new ServiceResponse<ExchangeHistoryDto>
                {
                    Data = exchange,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<ExchangeHistoryDto>
                {
                    Data = exchange,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<ExchangeHistoryDto>>> GetExchangeHistory()
        {
            var majorList = await _exchangeHistoryRepository.GetAllAsync<ExchangeHistoryDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<ExchangeHistoryDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<ExchangeHistoryDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        

        public async Task<ServiceResponse<GetExchangeHistoryDto>> GetExchangeHistoryById(Guid eventId)
        {
            try
            {

                var eventDetail = await _exchangeHistoryRepository.GetAsync<GetExchangeHistoryDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<GetExchangeHistoryDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<GetExchangeHistoryDto>
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

        public async Task<ServiceResponse<bool>> UpdateExchangeHistory(Guid id, UpdateExchangeHistoryDto exchangeHistoryDto)
        {
            try
            {

                exchangeHistoryDto.ExchangeDate = DateTime.UtcNow;

                await _exchangeHistoryRepository.UpdateAsync(id, exchangeHistoryDto);
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
            return await _exchangeHistoryRepository.Exists(id);
        }
    }
}
