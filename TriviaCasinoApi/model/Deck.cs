namespace TriviaCasinoApi.Model;
public class Deck {
    public Stack<Card> Cards { get; set; } = new();
    public DeckType deckType;

    public Deck ShuffleNewDeck() {
        switch(deckType) {
            case DeckType.STANDARD:
                CreateStandardDeck();
                break;
            default:
                CreateStandardDeck();
                break;
        }
        ShuffleDeck();

        return this;
    }

    public void ShuffleDeck() {
        List<Card> cardList = Cards.ToList();
        Random rng = new();

        for (int i = cardList.Count - 1; i > 0; i--) {
            int j = rng.Next(i + 1);
            (cardList[i], cardList[j]) = (cardList[j], cardList[i]);
        }

        Cards = new Stack<Card>(cardList);
    }

    public Deck CreateStandardDeck() {
        List<Card> cardList = [];
        deckType = DeckType.STANDARD;

        foreach (string suit in STANDARD_SUITS) {
            foreach (var cardKvp in STANDARD_DECK_CARDS) {
                foreach (var cardValue in cardKvp.Value) {
                    cardList.Add(new Card(cardKvp.Key, cardValue, suit));
                }
            }
        }
    
        Cards = new Stack<Card>(cardList);
        return this;
    }

    public List<Card> DrawCards(int numberOfCards) {
        List<Card> cards = [];

        if (numberOfCards <= 0) {
            throw new InvalidOperationException("Need a positive number of cards to draw.");
        }

        for (int i = 0; i < numberOfCards; i++) {
            cards.Add(DrawCard());
        }

        return cards;
    }

    public Card DrawCard() {
        if (Cards.Count == 0) {
            throw new InvalidOperationException("The deck is empty");
        }

        return Cards.Pop();
    }

    private readonly Dictionary<int, List<string>> STANDARD_DECK_CARDS = new() {
        { 2, new List<string> { "2" } },
        { 3, new List<string> { "3" } },
        { 4, new List<string> { "4" } },
        { 5, new List<string> { "5" } },
        { 6, new List<string> { "6" } },
        { 7, new List<string> { "7" } },
        { 8, new List<string> { "8" } },
        { 9, new List<string> { "9" } },
        { 10, new List<string> { "10", "J", "Q", "K" } },
        { 11, new List<string> { "A" } }
    };

    private readonly List<string> STANDARD_SUITS = [
        "Spade",
        "Heart",
        "Club",
        "Diamond"
    ];
}
