namespace TriviaCasino.Model;
public abstract class ACardGame : AGame {
    protected Deck deck { set; get; } = new();
    protected Dictionary<string, Hand> playerHands = new();

    public override void PlayAgain() {
        deck.ShuffleDeck();
        playerHands.Clear();
    }

    public abstract void DealStartingCards();
}
