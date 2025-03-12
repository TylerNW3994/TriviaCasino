namespace TriviaCasinoAPI.Model;
public class Card {
    public int value { get; set; }
    public string rank { get; set; } = "";
    public CardSuit suit { get; set; }

    public Card(int newValue, string newRank, CardSuit newSuit) {
        value = newValue;
        rank = newRank;
        suit = newSuit;
    }
}
