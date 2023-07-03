using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.QuestionDto;
using DataAccess.Repositories.MajorRepositories;
using DataAccess.Repositories.QuestionRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewQuestion(CreateQuestionDto createQuestionDto)
        {
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Question>(createQuestionDto);
            eventTaskcreate.Id = Guid.NewGuid();
            await _questionRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<IEnumerable<GetQuestionDto>>> GetQuestion()
        {
            var majorList = await _questionRepository.GetAllAsync<GetQuestionDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetQuestionDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetQuestionDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<QuestionDto>> GetQuestionById(Guid eventId)
        {
            try
            {

                var eventDetail = await _questionRepository.GetAsync<QuestionDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<QuestionDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<QuestionDto>
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

        public async Task<ServiceResponse<string>> UpdateQuestion(Guid id, UpdateQuestionDto questionDto)
        {
            try
            {
                await _questionRepository.UpdateAsync(id, questionDto);
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
            return await _questionRepository.Exists(id);
        }
    }
}
