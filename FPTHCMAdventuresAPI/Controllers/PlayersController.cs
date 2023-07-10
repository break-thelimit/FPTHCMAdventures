﻿using AutoMapper;
using DataAccess.Dtos.PlayerHistoryDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.PlayerHistoryService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.PlayerService;
using DataAccess.Dtos.PlayerDto;


namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayersController(IMapper mapper, IPlayerService playerService)
        {
            this._mapper = mapper;
            _playerService = playerService;
        }


        [HttpGet(Name = "GetPlayer")]

        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> GetPlayerList()
        {
            try
            {
                var res = await _playerService.GetPlayer();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerById(Guid id)
        {
            var eventDetail = await _playerService.GetPlayerById(id);
            return Ok(eventDetail);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerByUserId(Guid id)
        {
            var eventDetail = await _playerService.GetPlayerByUserId(id);
            return Ok(eventDetail);
        }
        [HttpGet("player/{email}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerByUserId(string email)
        {
            var eventDetail = await _playerService.CheckPlayerByUserName(email);
            return Ok(eventDetail);
        }

        [HttpPost("player", Name = "CreateNewPlayer")]

        public async Task<ActionResult<ServiceResponse<PlayerDto>>> CreateNewPlayer(CreatePlayerDto answerDto)
        {
            try
            {
                var res = await _playerService.CreateNewPlayer(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<PlayerDto>>> UpdatePlayer(Guid id, [FromBody] UpdatePlayerDto eventDto)
        {
            try
            {
                var res = await _playerService.UpdatePlayer(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);

            }
        }
    }
}
