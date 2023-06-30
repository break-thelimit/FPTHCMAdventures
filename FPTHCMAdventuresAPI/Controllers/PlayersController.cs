using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Model;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        IPlayerRepository playerRepository = new PlayerRepository();
        private readonly IConfiguration Configuration;

        public PlayersController(IConfiguration config)
        {
            Configuration = config;
        }

        // GET: api/Players
        [HttpGet]
        public JsonResult GetPlayers()
        {
            IEnumerable<Player> list = playerRepository.GetPlayers();
            return new JsonResult(new
            {
                result = list
            });
        }

        // GET: api/Players/5
        [HttpGet("getplayer/{playerid}")]
        public JsonResult GetPlayer(Guid id)
        {
            Player player = playerRepository.GetPlayerByID(id);
            return new JsonResult(new
            {
                result = player
            });
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
/*        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(Guid id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("addnewplayer/{playerid}")]
        public JsonResult CreatePlayer(Guid id)
        {
            Player tmp = playerRepository.AddNewPlayer(id);
            if (tmp != null)
            {
                return new JsonResult(new
                {
                    status = 200, // created successfully!
                    player_id = tmp.Id,
                    message = "Created successfully!"
                });
            }

            else
            {
                return new JsonResult(new
                {
                    status = 406, // created failed with invalid input value!
                    message = "This player has existed in system!"
                });
            }
        }
    }
}
