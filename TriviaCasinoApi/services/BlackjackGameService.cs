using TriviaCasinoApi.Model;
namespace TriviaCasinoApi.services;

public class BlackjackGameService : GameService {
    public BlackjackGame Hit(string gameId, Player player) {
        BlackjackGame game = (GetGame(gameId) as BlackjackGame)!;

        game.Hit(player.Username);

        UpdateGame(gameId, game);

        return game;
    }

    public BlackjackGame Stand(string gameId, Player player) {
        BlackjackGame game = (GetGame(gameId) as BlackjackGame)!;
        game.Stand(player.Username);

        return game;
    }
}
