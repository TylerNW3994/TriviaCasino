using System.ComponentModel.DataAnnotations;

public class Player {
    [Key]
    public int UserId { set; get; }
    public string Password { set; get; } = "";
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
}
