namespace TriviaCasinoAPI.Model;
public abstract class ACardGame : AGame {
    public Deck Deck { set; get; } = new();
    public Dictionary<string, List<Card>> PlayerHands { get; set; } = new();

    public override void PlayAgain() {
        Deck.ShuffleDeck();
        PlayerHands.Clear();
    }

    public abstract void DealStartingCards();
}
