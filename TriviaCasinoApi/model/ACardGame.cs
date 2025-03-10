namespace TriviaCasinoAPI.Model;
public abstract class ACardGame : AGame {
    protected Deck deck { set; get; } = new();
    protected Dictionary<string, Hand> playerHands { get; set; } = new();

    public override void PlayAgain() {
        deck.ShuffleDeck();
        playerHands.Clear();
    }

    public abstract void DealStartingCards();
}
