using TriviaCasinoApi.Model;
namespace TriviaCasino.Tests;

public class DeckTest {
    private Deck deck;

    public DeckTest() {
        deck = new();
    }

    [Fact]
    public void DeckShouldInitialize() {
        Assert.NotNull(deck);
    }

    [Fact]
    public void StandardDeckShouldHave52Cards() {
        deck.CreateStandardDeck();

        Assert.Equal(52, deck.Cards.Count);
    }

    [Fact]
    public void ShouldDrawCard() {
        deck.CreateStandardDeck();

        Card card = deck.DrawCard();

        Assert.NotNull(card);
    }

    [Fact]
    public void ShouldDrawMultipleCards() {
        deck.CreateStandardDeck();

        List<Card> cards = deck.DrawCards(3);

        Assert.NotNull(cards);
        Assert.Equal(3, cards.Count);
    }

    [Fact]
    public void ShouldThrowExceptionZeroNegativeMultipleCards() {
        deck.CreateStandardDeck();

        Assert.Throws<InvalidOperationException>(() => deck.DrawCards(0));
    }

    [Fact]
    public void DrawFromEmptyDeckShouldThrow() {
        Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
    }

    [Fact]
    public void DeckCountShouldDecreaseAfterDrawing() {
        deck.CreateStandardDeck();
        int initialCount = deck.Cards.Count;
        deck.DrawCard();
        Assert.Equal(initialCount - 1, deck.Cards.Count);
    }

    [Fact]
    public void ShuffleNewDeckShouldReturnSameDeckInstance() {
        var result = deck.ShuffleNewDeck();
        Assert.Same(deck, result);
    }

    [Fact]
    public void ShuffleShouldChangeCardOrder() {
        deck.CreateStandardDeck();
        var originalOrder = deck.Cards.ToArray();
        deck.ShuffleDeck();
        var shuffledOrder = deck.Cards.ToArray();
        // There's a very small chance the order remains the same after shuffling, so shuffle again.
        // If it's still the same, it's meant to be... or something is wrong.
        if (originalOrder == shuffledOrder) {
            deck.ShuffleDeck();
            shuffledOrder = deck.Cards.ToArray();
        }
        
        Assert.NotEqual(originalOrder, shuffledOrder);
    }

    [Fact]
    public void CreateStandardDeckShouldSetDeckTypeToStandard() {
        deck.CreateStandardDeck();
        Assert.Equal(DeckType.STANDARD, deck.deckType);
    }

    [Fact]
    public void DrawingAllCardsEmptiesDeck() {
        deck.CreateStandardDeck();
        int initialCount = deck.Cards.Count;

        for (int i = 0; i < initialCount; i++) {
            deck.DrawCard();
        }
        Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
    }
}
