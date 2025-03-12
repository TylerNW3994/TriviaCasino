using TriviaCasinoAPI.Model;

public class BlackjackGameService {
    private Dictionary<string, BlackjackGame> _games = new();

    public BlackjackGame CreateNewGame(string gameId, Player initialPlayer) {
        var game = new BlackjackGame(gameId);
        
        if (initialPlayer != null) {
            game.AddPlayer(initialPlayer);
            
            if (game.CurrentPlayer == string.Empty) {
                game.CurrentPlayer = initialPlayer.Username;
            }
        }
        
        game.Initialize();
        game.StartGame();
        game.DealStartingCards();

        _games[gameId] = game;
        return game;
    }

    public BlackjackGame? GetGame(string gameId) {
        return _games.TryGetValue(gameId, out var game) ? game : null;
    }
    // Additional methods for Hit, Stand, etc.
}
