using Microsoft.AspNetCore.Mvc;
using TournamentPlanner.Services;

namespace TournamentPlanner.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly MatchService matchService;

        public MatchController(MatchService matchService)
        {
            this.matchService = matchService;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddMatch(int player1Id, int player2Id)
        {
            if (player1Id < 0 || player2Id < 0) return BadRequest("Invalid Query Parameters");
            return Ok(matchService.AddMatch(player1Id, player2Id));
        }

        [HttpPut]
        [Route("setWinner")]
        public IActionResult SetWinner(int matchId, int playerId)
        {
            if (matchId < 0 || playerId < 0) return BadRequest("Invalid Query Parameters");
            return Ok(matchService.SetWinner(matchId, playerId));
        }

        [HttpGet]
        [Route("openMatches")]
        public IActionResult GetOpenMatches()
        {
            return Ok(matchService.GetOpenMatches());
        }

        [HttpDelete]
        public IActionResult DeleteAllMatches()
        {
            return Ok(matchService.DeleteAllMatches());
        }
    }
}
