public abstract class ACardGame : AGame {
    public Deck deck { set; get; } = new();
    Dictionary<string, Hand> playerHands = new();

    public override void PlayAgain() {
        deck.ShuffleDeck();
        playerHands.Clear();
    }

    private void DealCards();
}
