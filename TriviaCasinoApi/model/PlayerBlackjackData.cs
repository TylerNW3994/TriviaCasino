namespace TriviaCasinoAPI.Model;
public class PlayerBlackjackData {
    public int Score { get; set; }
    public int Status { get; set; }
    public int Chips { get; set; }
    public List<Card>? Hand { get; set; }
}
