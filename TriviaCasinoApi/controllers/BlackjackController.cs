using Microsoft.AspNetCore.Mvc;
using TriviaCasino.Model;

namespace TriviaCasinoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BlackjackController : ControllerBase {
        BlackjackGame game = new();
        [HttpPost("newgame")]
        public IActionResult NewGame([FromBody] GameActionRequest request) {
            game = new BlackjackGame();
            game.Initialize();
            game.StartGame();
            game.DealStartingCards();
            return Ok(game);
        }

        [HttpPost("hit")]
        public IActionResult Hit([FromBody] GameActionRequest request) {
            game.Hit(request.Username);
            return Ok(game);
        }
    }
}
