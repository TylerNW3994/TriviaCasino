using Microsoft.Identity.Client;
using TriviaCasinoApi.Model;

public class GameService {
    private Dictionary<string, AGame> games { get; set; } = new();

    public AGame CreateNewGame(GameType gameType, string gameId, Player player) {
        AGame game;
        switch (gameType) {
            case GameType.Blackjack:
                game = new BlackjackGame(gameId);
                break;
            // case GameType.Poker:
            //     game = new PokerGame(gameId);
            //     break;
            // case GameType.Slots:
            //     game = new SlotsGame(gameId);
            //     break;
            // Add more game types as needed
            default:
                throw new ArgumentException("Invalid game type");
        }
        
        if (player != null) {
            game.AddPlayer(player);
            
            if (game.CurrentPlayer == string.Empty) {
                game.CurrentPlayer = player.Username;
            }
        }
        
        game.Initialize();
        game.StartGame();

        UpdateGame(gameId, game);
        return game;
    }
    
    protected void UpdateGame(string gameId, AGame game) => games[gameId] = game;
    protected void RemoveGame(string gameId) => games.Remove(gameId);
    protected AGame GetGame(string gameId) => games.TryGetValue(gameId, out var game) ? game : throw new InvalidOperationException($"Game not found with Id: {gameId}");
}
