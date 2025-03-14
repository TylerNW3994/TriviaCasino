namespace TriviaCasinoAPI.Model;
public class Card {
    public int Value { get; set; }
    public string Rank { get; set; } = "";
    public string Suit { get; set; }

    public Card(int value, string rank, string suit) {
        this.Value = value;
        this.Rank = rank;
        this.Suit = suit;
    }
}
