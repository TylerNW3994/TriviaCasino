namespace TriviaCasinoAPI.Model;

public class PlayerDTO {
    public string Username { get; set; } = "";
    public List<Card>? Hand { get; set; }
    public int Score { get; set; }
    public int Bet { get; set; }
    public int Chips { get; set; }
    public string Status { get; set; } = "";
}
