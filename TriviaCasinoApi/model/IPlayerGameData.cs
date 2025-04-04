namespace TriviaCasinoAPI.Model;
public interface IPlayerGameData {
    int Bet { get; }
    double BetMultiplier { get; }
    void SetChips(int chips);
}
