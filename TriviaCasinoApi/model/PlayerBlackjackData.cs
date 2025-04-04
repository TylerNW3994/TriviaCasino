namespace TriviaCasinoAPI.Model;
public class PlayerBlackjackData : IPlayerGameData {
    public int Score { get; set; }
    public double Status { get; set; }
    public int Chips { get; set; }
    public int Bet { get; set; }
    public List<Card>? Hand { get; set; }
    public double BetMultiplier => Status;

    public void SetChips(int chips) {
        Chips = chips;
    }
}
