using AutoMapper;
using DataAccess.Dtos.ItemDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.ItemService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.PlayerHistoryService;
using DataAccess.Dtos.PlayerHistoryDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerHistorysController : ControllerBase
    {
        private readonly IPlayerHistoryService _playerHistoryService;
        private readonly IMapper _mapper;

        public PlayerHistorysController(IMapper mapper, IPlayerHistoryService playerHistoryService)
        {
            this._mapper = mapper;
            _playerHistoryService = playerHistoryService;
        }


        [HttpGet(Name = "GetPlayerHistory")]

        public async Task<ActionResult<ServiceResponse<GetPlayerHistoryDto>>> GetPlayerHistoryList()
        {
            try
            {
                var res = await _playerHistoryService.GetPlayerHistory();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerHistoryDto>> GetItemById(Guid id)
        {
            var eventDetail = await _playerHistoryService.GetPlayerHistoryById(id);
            return Ok(eventDetail);
        }

        [HttpPost("playerhistory", Name = "CreateNewPlayerHistory")]

        public async Task<ActionResult<ServiceResponse<PlayerHistoryDto>>> CreateNewPlayerHistory(CreatePlayerHistoryDto answerDto)
        {
            try
            {
                var res = await _playerHistoryService.CreateNewPlayerHistory(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<PlayerHistoryDto>>> UpdatePlayerHistory(Guid id, [FromBody] UpdatePlayerHistoryDto eventDto)
        {
            try
            {
                var res = await _playerHistoryService.UpdatePlayerHistory(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
