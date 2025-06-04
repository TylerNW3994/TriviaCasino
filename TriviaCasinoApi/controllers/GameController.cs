using Microsoft.AspNetCore.Mvc;
using TriviaCasinoApi.Model;

namespace TriviaCasinoApi.Controllers {
    [ApiController]
    [Route("api/Game")]
    public class GameController : ControllerBase {
        private readonly GameService service;

        public GameController(GameService gameService) {
            service = gameService;
        }

        [HttpPost("newgame")]
        public ApiResponse NewGame([FromBody] GameActionRequest request) {
            try {
                AGame game = service.CreateNewGame(request.GameState.GameType, Guid.NewGuid().ToString(), request.Player);

                var gameDto = game.ToApiResponseDto();
                var response = new ApiResponse(gameDto);
                return response;
            }
            catch (Exception e) {
                Console.WriteLine($"Error creating new game: {e.Message}");
                throw new InvalidOperationException(e.ToString());
            }
            
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
