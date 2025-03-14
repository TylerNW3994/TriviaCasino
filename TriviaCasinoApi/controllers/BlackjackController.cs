using Microsoft.AspNetCore.Mvc;
using TriviaCasinoAPI.Model;

namespace TriviaCasinoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BlackjackController : ControllerBase {
        private readonly BlackjackGameService service;

        public BlackjackController(BlackjackGameService gameService) {
            service = gameService;
        }

        [HttpPost("newgame")]
        public ApiResponse NewGame([FromBody] GameActionRequest request) {
            BlackjackGame game = service.CreateNewGame(Guid.NewGuid().ToString(), request.UserToAdd);

            var response = new ApiResponse {
                GameData = game.ToDto(),
                Message = "New Game Started"
            };
            return response;
        }

        [HttpPost("hit")]
        public ApiResponse Hit([FromBody] GameActionRequest request) {
            BlackjackGame game = service.CreateNewGame(Guid.NewGuid().ToString(), request.UserToAdd);

            var response = new ApiResponse {
                GameData = game.ToDto(),
                Message = request.Username + " hit!"
            };
            return response;
        }

        [HttpPost("stand")]
        public ApiResponse Stand([FromBody] GameActionRequest request) {
            BlackjackGame game = service.CreateNewGame(Guid.NewGuid().ToString(), request.UserToAdd);

            var response = new ApiResponse {
                GameData = game.ToDto(),
                Message = request.Username + " stands! Moving to next player..."
            };
            return response;
        }
    }

    public class ApiResponse {
        public required BlackjackGameDto GameData { get; set; }
        public required string Message { get; set; }
    }

    public class GameActionRequest {
        public required BlackjackGame GameState { get; set; } = new("");
        public required string Username { get; set; } = string.Empty;
        public Player? UserToAdd { get; set; }
    }
}
