using TriviaCasinoAPI.Model;

public class BlackjackGameService {
    private Dictionary<string, BlackjackGame> _games = new();

    public BlackjackGame CreateNewGame(string gameId, Player player) {
        var game = new BlackjackGame(gameId);
        
        if (player != null) {
            game.AddPlayer(player);
            
            if (game.CurrentPlayer == string.Empty) {
                game.CurrentPlayer = player.Username;
            }
        }
        
        game.Initialize();
        game.StartGame();

        _games[gameId] = game;
        return game;
    }

    public BlackjackGame Hit(string gameId , Player player) {
        BlackjackGame game = _games[gameId];

        List<Card> hand = game.PlayerHands[player.Username];

        hand.Add(game.Deck.DrawCard());
        _games[gameId] = game;

        return game;
    }

    public BlackjackGame Stand(string gameId) {
        BlackjackGame game = _games[gameId];
        game.NextPlayer();

        return game;
    }

    public BlackjackGame? GetGame(string gameId) {
        return _games.TryGetValue(gameId, out var game) ? game : null;
    }
}
