using Microsoft.AspNetCore.Mvc;
using TriviaCasino.Model;

namespace TriviaCasinoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BlackjackController : ControllerBase {
        BlackjackGame game = new();
        [HttpPost("newgame")]
        public ApiResponse NewGame([FromBody] GameActionRequest request) {
            game = new BlackjackGame();
            game.Initialize();
            game.StartGame();
            game.DealStartingCards();            

            var response = new ApiResponse {
                GameData = game,
                Message = "New Game Started"
            };
            return response;
        }

        [HttpPost("hit")]
        public ApiResponse Hit([FromBody] GameActionRequest request) {
            var response = new ApiResponse {
                GameData = game,
                Message = request.Username + " hit!"
            };
            return response;
        }
    }

    public class ApiResponse {
        public BlackjackGame GameData { get; set; }
        public string Message { get; set; }
    }
}
