namespace TriviaCasinoAPI.Model;
public abstract class ACardGame : AGame {
    protected Deck Deck { set; get; } = new();
    protected Dictionary<string, List<Card>> PlayerHands { get; set; } = new();

    public override void PlayAgain() {
        Deck.ShuffleDeck();
        PlayerHands.Clear();
    }

    public abstract void DealStartingCards();
}
