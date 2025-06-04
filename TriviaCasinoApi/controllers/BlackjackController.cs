using Microsoft.AspNetCore.Mvc;
using TriviaCasinoApi.Model;
using TriviaCasinoApi.services;

namespace TriviaCasinoApi.Controllers;
[ApiController]
[Route("api/Blackjack")]
public class BlackjackController : GameController {
    private readonly BlackjackGameService service;

    public BlackjackController(BlackjackGameService gameService) : base(gameService) {
        service = gameService;
    }

    [HttpPost("hit")]
    public ApiResponse Hit([FromBody] GameActionRequest request) {
        BlackjackGame game = service.Hit(request.GameState.GameId, request.Player);

        var response = new ApiResponse(game.ToApiResponseDto());
        return response;
    }

    [HttpPost("stand")]
    public ApiResponse Stand([FromBody] GameActionRequest request) {
        BlackjackGame game = service.Stand(request.GameState.GameId);

        var response = new ApiResponse(game.ToApiResponseDto());
        return response;
    }
}
