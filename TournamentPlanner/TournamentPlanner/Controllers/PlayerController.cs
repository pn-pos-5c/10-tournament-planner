using Microsoft.AspNetCore.Mvc;
using TournamentPlanner.DTOs;
using TournamentPlanner.Services;

namespace TournamentPlanner.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService playerService;

        public PlayerController(PlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpPost]
        public IActionResult AddPlayer([FromBody] PlayerDto player)
        {
            if (player == null) return BadRequest("Invalid Request Body");
            return Ok(playerService.AddPlayer(player));
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(playerService.GetPlayers());
        }
    }
}
