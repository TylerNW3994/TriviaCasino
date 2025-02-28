using Microsoft.AspNetCore.Mvc;
using TriviaCasino.Model;

namespace TriviaCasinoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BlackjackController : ControllerBase {
        [HttpPost("newgame")]
        public IActionResult NewGame() {
            var game = new BlackjackGame();
            return Ok(game);
        }

        [HttpPost("hit")]
        public IActionResult Hit([FromBody] BlackjackGame game, string username) {
            game.Hit(username);
            return Ok(game);
        }
    }
}