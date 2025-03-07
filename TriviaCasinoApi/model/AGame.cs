namespace TriviaCasinoAPI.Model;
public abstract class AGame {
    public int Id;
    public string Name { set; get; } = "";
    public string Winner { set; get; } = "";
    public GameState Status { protected set; get; } = GameState.NotStarted;
    public List<Player> Players { set; get; } = new();
    public string CurrentPlayer { get; set; } = string.Empty;

    public abstract void DetermineWinner();
    public abstract void PlayAgain();

    public void EndGame() {
        Status = GameState.Completed;
    }

    public void AddPlayer(Player player) {
        Players.Add(player);
    }

    public void RemovePlayer(Player player) {
        Players.Remove(player);
    }

    public Player GetPlayerByUsername(string username) {
        Player? player = Players.Find(p => p.Username == username);

        if (player == null) {
            throw new InvalidOperationException("Player not found with username: " + username);
        }
        return player;
    }

    public Player? GetNextPlayer() {
        if (CurrentPlayer == string.Empty) {
            Player? player = Players[0] ?? throw new InvalidOperationException("Game has no Players");
            return player;
        } else {
            int currentPlayerIndex = Players.FindIndex(p => p.Username == CurrentPlayer);

            // Dealer's turn
            if (currentPlayerIndex == Players.Count - 1) {
                CurrentPlayer = string.Empty;
                return null;
            }

            Player? nextPlayer = Players[currentPlayerIndex + 1];
            CurrentPlayer = nextPlayer.Username;
            return Players[currentPlayerIndex + 1];
        }
    }
}
