using System.ComponentModel.DataAnnotations;
namespace TriviaCasinoApi.Model;

public class Player {
    [Key]
    public int UserId { set; get; }
    private string Password { set; get; } = "";
    public string Username { set; get; } = "";
    public string Email { set; get; } = "";
    public int GamesWon { private set; get; }
    public int GamesPlayed { private set; get; }
    public int Chips { set; get; }
    public int Bet { set; get; }
    public double BetMultiplier { set; get; }

    public void IncreaseGamesWon() {
        GamesWon++;
    }

    public void IncreaseGamesPlayed() {
        GamesPlayed++;
    }

    public PlayerCardGameDTO ToPlayerCardGameDto(PlayerBlackjackData data) {
        return new PlayerCardGameDTO
        {
            Username = Username,
            Hand = data.Hand,
            Score = data.Score,
            Bet = data.Bet,
            Chips = data.Chips
        };
    }
}
