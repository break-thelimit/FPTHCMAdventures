using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.AnswerDto;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.LocationDto;
using DataAccess.Repositories.AnswerRepositories;
using DataAccess.Repositories.EventRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AnswerService
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewAnswer(CreateAnswerDto createEventDto)
        {
            var mapper = config.CreateMapper();
            var eventcreate = mapper.Map<Answer>(createEventDto);
            eventcreate.Id = Guid.NewGuid();
            await _answerRepository.AddAsync(eventcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetAnswerDto>>> GetAnswer()
        {
            var eventList = await _answerRepository.GetAllAnswerkAsync();
            if (eventList != null)
            {
                return new ServiceResponse<IEnumerable<GetAnswerDto>>
                {
                    Data = eventList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetAnswerDto>>
                {
                    Data = eventList,
                    Success = false,
                    Message = "Faile because List answer null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<AnswerDto>> GetAnswerById(Guid eventId)
        {
            try
            {

                var eventDetail = await _answerRepository.GetAsync<AnswerDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<AnswerDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<AnswerDto>
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

        public async Task<ServiceResponse<PagedResult<AnswerDto>>> GetAnswerWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _answerRepository.GetAllAsync<AnswerDto>(queryParameters);
            return new ServiceResponse<PagedResult<AnswerDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<string>> UpdateAnswer(Guid id, UpdateAnswerDto eventDto)
        {
            if (id != eventDto.Id)
            {
                return new ServiceResponse<string>
                {
                    Message = "Invalid Record Id",
                    Success = false,
                    StatusCode = 500
                };

            }
            var checkEvent = await _answerRepository.GetAsync(id);
            if (checkEvent == null)
            {
                return new ServiceResponse<string>
                {
                    Message = "No rows",
                    Success = false,
                    StatusCode = 500
                };
            }
            else
            {
                _mapper.Map(eventDto, checkEvent);

                try
                {
                    await _answerRepository.UpdateAsync(checkEvent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CountryExists(id))
                    {
                        return new ServiceResponse<string>
                        {
                            Message = "No rows",
                            Success = false,
                            StatusCode = 500
                        };
                    }
                    else
                    {
                        throw;
                    }
                }
                return new ServiceResponse<string>
                {
                    Message = "Update Success",
                    Success = true,
                    StatusCode = 500
                };
            }
        }

        private async Task<bool> CountryExists(Guid id)
        {
            return await _answerRepository.Exists(id);
        }
    }
}
