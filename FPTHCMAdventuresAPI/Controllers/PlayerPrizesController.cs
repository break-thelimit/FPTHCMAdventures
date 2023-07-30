using AutoMapper;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.StudentDto;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.StudentService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.PlayerPrizeService;
using DataAccess.Dtos.PlayerPrizeDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerPrizesController : ControllerBase
    {
        private readonly IPlayerPrizeService _playerPrizeService;
        private readonly IMapper _mapper;

        public PlayerPrizesController(IMapper mapper, IPlayerPrizeService playerPrizeService)
        {
            this._mapper = mapper;
            _playerPrizeService = playerPrizeService;
        }
        [HttpGet(Name = "GetPlayerPrize")]

        public async Task<ActionResult<ServiceResponse<PlayerPrizeDto>>> GetPlayerPrizeList()
        {
            try
            {
                var res = await _playerPrizeService.GetPlayerPrize();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerPrizeDto>> GetPlayerPrizeById(Guid id)
        {
            var eventDetail = await _playerPrizeService.GetPlayerPrizeById(id);
            return Ok(eventDetail);
        }

        [HttpPost("playerprize", Name = "CreateNewPlayerPrize")]

        public async Task<ActionResult<ServiceResponse<GetPlayerPrizeDto>>> CreateNewPlayerPrize(CreatePlayerPrizeDto playerPrizeDto)
        {
            try
            {
                var res = await _playerPrizeService.CreateNewPlayerPrize(playerPrizeDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<GetPlayerPrizeDto>>> UpdatePlayerPrize(Guid id, [FromBody] UpdatePlayerPrizeDto playerPrizeDto)
        {
            try
            {
                var res = await _playerPrizeService.UpdatePlayerPrize(id, playerPrizeDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}