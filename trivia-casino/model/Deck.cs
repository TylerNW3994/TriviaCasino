public class Deck {
    public Stack<Card> cards = new();
    public DeckType deckType;

    public Deck ShuffleNewDeck() {
        switch(deckType) {
            case DeckType.STANDARD: 
                CreateStandardDeck();
                break;
        }
        ShuffleDeck();

        return this;
    }

    public void ShuffleDeck() {
        Random rng = new Random();
        for (int i = cards.Count - 1; i > 0; i--) {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public Deck CreateStandardDeck() {
        List<Card> cardList = new();
        deckType = DeckType.STANDARD;

        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit))) {
            foreach (var cardKvp in STANDARD_DECK_CARDS) {
                foreach (var cardValue in cardKvp.Value) {
                    Card newCard = new Card(cardKvp.Key, cardValue, suit);
                    cardList.Add(newCard);
                }
            }
        }
        
        cards = new Stack<Card>(cardList);
        return this;
    }

    public Card DrawCard() {
        if (cards.Count == 0) {
            throw new InvalidOperationException("The deck is empty");
        }

        return cards.Pop();
    }

    private Dictionary<int, List<string>> STANDARD_DECK_CARDS = new Dictionary<int, List<string>> {
        { 1, new List<string> { "1" } },
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
}