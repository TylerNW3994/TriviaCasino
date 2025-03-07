using Microsoft.AspNetCore.Mvc;
using TriviaCasinoAPI.Model;

namespace TriviaCasinoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BlackjackController : ControllerBase {
        BlackjackGame game = new();
        [HttpPost("newgame")]
        public ApiResponse NewGame([FromBody] GameActionRequest request) {
            game = new BlackjackGame();

            if (request.UserToAdd != null) {
                game.AddPlayer(request.UserToAdd);
                
                if (game.CurrentPlayer == string.Empty) {
                    game.CurrentPlayer = request.UserToAdd.Username;
                }
            }
            
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

        [HttpPost("stand")]
        public ApiResponse Stand([FromBody] GameActionRequest request) {
            var response = new ApiResponse {
                GameData = game,
                Message = request.Username + " stands! Moving to next player..."
            };
            return response;
        }
    }

    public class ApiResponse {
        public required BlackjackGame GameData { get; set; }
        public required string Message { get; set; }
    }

    public class GameActionRequest {
        public required BlackjackGame GameState { get; set; } = new();
        public required string Username { get; set; } = string.Empty;
        public Player? UserToAdd {get; set; }
    }
}
