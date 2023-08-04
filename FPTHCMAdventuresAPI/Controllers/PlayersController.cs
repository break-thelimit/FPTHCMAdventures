﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult<ServiceResponse<PlayerDto>>> GetPlayerList()
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

        [HttpGet("GetRankedPlayer")]

        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> GetRankedPlayer(Guid eventId,Guid schoolId)
        {
            try
            {
                var res = await _playerService.GetRankedPlayer(eventId,schoolId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        /*   [HttpGet("players/listPlayer-username", Name = "GetPlayerWithUserNames")]

           public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> GetPlayerListWithUserName()
           {
               try
               {
                   var res = await _playerService.GetPlayerWithUserName();
                   return Ok(res);
               }
               catch (Exception ex)
               {
                   return StatusCode(500, "Internal server error: " + ex.Message);
               }
           }*/
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerById(Guid id)
        {
            var eventDetail = await _playerService.GetPlayerById(id);
            return Ok(eventDetail);
        }

        [HttpGet("user/{studentId}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerByStudentId(Guid studentId)
        {
            var eventDetail = await _playerService.GetPlayerByStudentId(studentId);
            return Ok(eventDetail);
        }
        [HttpGet("player/player-{nickname}")]
        public async Task<ActionResult<GetPlayerDto>> GetPlayerByNickName(string nickname)
        {
            var eventDetail = await _playerService.CheckPlayerByNickName(nickname);
            return Ok(eventDetail);
        }
      /*  [HttpGet("player/{username}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerByUserName(string username)
        {
            var eventDetail = await _playerService.CheckPlayerByUserName(username);
            return Ok(eventDetail);
        }*/

        [HttpPost("player", Name = "CreateNewPlayer")]

        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> CreateNewPlayer(CreatePlayerDto createPlayerDto)
        {
            try
            {
                var res = await _playerService.CreateNewPlayer(createPlayerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> UpdatePlayer(Guid id, [FromBody] UpdatePlayerDto eventDto)
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