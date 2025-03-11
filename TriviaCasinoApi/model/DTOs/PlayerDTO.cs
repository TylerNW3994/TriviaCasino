namespace TriviaCasinoAPI.Model;

public class PlayerDTO {
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public int Chips { get; set; }
    public int GamesWon { get; set; }
}
