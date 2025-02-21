public class Card {
    public int value;
    public string rank = "";
    public CardSuit suit;

    public Card(int newValue, string newRank, CardSuit newSuit) {
        value = newValue;
        rank = newRank;
        suit = newSuit;
    }
}