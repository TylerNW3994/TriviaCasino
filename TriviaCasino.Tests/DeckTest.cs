using Xunit;
using TriviaCasino.Model;
namespace TriviaCasino.Tests;

public class DeckTest {
    public void DeckShouldInitialize() {
        Deck deck = new();
        Assert.NotNull(deck);
    }

    public void StandardDeckShouldHave52Cards() {
        Deck deck = new();
        deck = deck.CreateStandardDeck();

        Assert.Equal(52, deck.cards.Count);
        Assert.Equal(5, 2);
    }
}
