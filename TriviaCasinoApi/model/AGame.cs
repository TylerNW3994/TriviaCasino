namespace TriviaCasinoApi.Model;
public abstract class AGame {
    public string GameId { set; get; } = "";
    public string Name { set; get; } = "";
    public string Winner { set; get; } = "";
    public GameState State { protected set; get; } = GameState.NotStarted;
    public List<Player> Players { set; get; } = [];
    public string CurrentPlayer { get; set; } = string.Empty;

    public abstract void DetermineWinner();
    public abstract void PlayAgain();
    public abstract void StartGame();
    public abstract void Initialize();
    public abstract GameDTO ToApiResponseDto();

    public void AdjustChips<T>(Dictionary<string, T> playerDatas) where T : IPlayerGameData {
        foreach (var player in Players) {
            var username = player.Username;
            if (!playerDatas.ContainsKey(username))
                continue;

            var data = playerDatas[username];
            player.Chips += (int)(data.Bet * data.BetMultiplier);
            data.SetChips(player.Chips);
        }
    }

    public void EndGame() {
        State = GameState.Completed;
    }

    public void AddPlayer(Player player) {
        Players.Add(player);
    }

    public void RemovePlayer(Player player) {
        Players.Remove(player);
    }

    public Player GetPlayerByUsername(string username) {
        Player? player = Players.Find(p => p.Username == username) ?? throw new InvalidOperationException("Player not found with username: " + username);
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
