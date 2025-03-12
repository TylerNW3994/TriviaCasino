using System.ComponentModel.DataAnnotations;
namespace TriviaCasinoAPI.Model;

public class Player {
    [Key]
    public int UserId { set; get; }
    private string Password { set; get; } = "";
    public string Username { set; get; } = "";
    public string Email { set; get; } = "";
    public int GamesWon { private set; get; }
    public int GamesPlayed { private set; get; }
    public int Chips { set; get; }

    public void IncreaseGamesWon() {
        GamesWon++;
    }

    public void IncreaseGamesPlayed() {
        GamesPlayed++;
    }

    public PlayerDTO ToDto() {
        return new PlayerDTO {
            UserId = UserId,
            Username = Username,
            Chips = Chips,
            GamesWon = GamesWon
        };
    }
}
