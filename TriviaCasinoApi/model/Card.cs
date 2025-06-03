namespace TriviaCasinoApi.Model;
public class Card {
    public int Value { get; set; }
    public string Rank { get; set; } = "";
    public string Suit { get; set; }

    public Card(int value, string rank, string suit) {
        Value = value;
        Rank = rank;
        Suit = suit;
    }
}
