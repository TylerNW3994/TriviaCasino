using Xunit;
using TriviaCasinoAPI.Model;
namespace TriviaCasino.Tests;

public class DeckTest {
    [Fact]
    public void DeckShouldInitialize() {
        Deck deck = new();
        Assert.NotNull(deck);
    }

    [Fact]
    public void StandardDeckShouldHave52Cards() {
        Deck deck = new();
        deck.CreateStandardDeck();

        Assert.Equal(52, deck.cards.Count);
    }

    [Fact]
    public void ShouldDrawCard() {
        Deck deck = new();
        deck.CreateStandardDeck();

        Card card = deck.DrawCard();

        Assert.NotNull(card);
    }

    [Fact]
    public void ShouldDrawMultipleCards() {
        Deck deck = new();
        deck.CreateStandardDeck();

        List<Card> cards = deck.DrawCards(3);

        Assert.NotNull(cards);
        Assert.Equal(3, cards.Count);
    }

    [Fact]
    public void ShouldThrowExceptionZeroNegativeMultipleCards() {
        Deck deck = new();
        deck.CreateStandardDeck();

        Assert.Throws<InvalidOperationException>(() => deck.DrawCards(0));
    }

    [Fact]
    public void DrawFromEmptyDeckShouldThrow() {
        Deck deck = new();
        Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
    }

    [Fact]
    public void DeckCountShouldDecreaseAfterDrawing() {
        Deck deck = new();
        deck.CreateStandardDeck();
        int initialCount = deck.cards.Count;
        deck.DrawCard();
        Assert.Equal(initialCount - 1, deck.cards.Count);
    }

    [Fact]
    public void ShuffleNewDeckShouldReturnSameDeckInstance() {
        Deck deck = new();
        var result = deck.ShuffleNewDeck();
        Assert.Same(deck, result);
    }

    [Fact]
    public void ShuffleShouldChangeCardOrder() {
        Deck deck = new();
        deck.CreateStandardDeck();
        var originalOrder = deck.cards.ToArray();
        deck.ShuffleDeck();
        var shuffledOrder = deck.cards.ToArray();
        // There's a very small chance the order remains the same after shuffling, so shuffle again.
        // If it's still the same, it's meant to be... or something is wrong.
        if (originalOrder == shuffledOrder) {
            deck.ShuffleDeck();
            shuffledOrder = deck.cards.ToArray();
        }
        
        Assert.NotEqual(originalOrder, shuffledOrder);
    }

    [Fact]
    public void CreateStandardDeckShouldSetDeckTypeToStandard() {
        Deck deck = new();
        deck.CreateStandardDeck();
        Assert.Equal(DeckType.STANDARD, deck.deckType);
    }

    [Fact]
    public void DrawingAllCardsEmptiesDeck() {
        Deck deck = new();
        deck.CreateStandardDeck();
        int initialCount = deck.cards.Count;

        for (int i = 0; i < initialCount; i++) {
            deck.DrawCard();
        }
        Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
    }
}
