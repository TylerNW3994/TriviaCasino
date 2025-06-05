using Microsoft.AspNetCore.Mvc;
using TriviaCasinoApi.Model;
using TriviaCasinoApi.services;

namespace TriviaCasinoApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BlackjackController : GameController {
    private readonly BlackjackGameService service;

    public BlackjackController(BlackjackGameService gameService) : base(gameService) {
        service = gameService;
    }

    [HttpPost("hit")]
    public ApiResponse Hit<TPlayerDTO>([FromBody] GameActionRequest request) where TPlayerDTO : APlayerDTO {
        BlackjackGame game = service.Hit(request.GameState.GameId, request.Player);

        var response = new ApiResponse(game.ToApiResponseDto());
        return response;
    }

    [HttpPost("stand")]
    public ApiResponse Stand<TPlayerDTO>([FromBody] GameActionRequest request) where TPlayerDTO : APlayerDTO  {
        BlackjackGame game = service.Stand(request.GameState.GameId);

        var response = new ApiResponse(game.ToApiResponseDto());
        return response;
    }
}
