using Microsoft.AspNetCore.Mvc;
using TriviaCasinoApi.Model;

namespace TriviaCasinoApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class GameController(GameService gameService) : ControllerBase {
        private readonly GameService service = gameService;

        [HttpPost("newgame")]
        public ApiResponse NewGame([FromBody] GameActionRequest request) {
            AGame game = service.CreateNewGame(request.GameState.GameType, Guid.NewGuid().ToString(), request.Player);

            var gameDto = game.ToApiResponseDto();
            var response = new ApiResponse(gameDto);
            return response;
        }
    }
    
    public class ApiResponse {
        public GameDTO GameData { get; }

        public ApiResponse(GameDTO gameData) {
            GameData = gameData ?? throw new ArgumentNullException(nameof(gameData));
        }
    }

    public class GameActionRequest {
        public required GameDTO GameState { get; set; }
        public required string Username { get; set; } = string.Empty;
        public Player Player { get; set; } = new();
    }
}
