public class BlackjackGame : ACardGame {
    Hand dealerHand = new();

    string currentPlayer = "";

    public void Initialize() {
        deck.deckType = DeckType.STANDARD;
    }

    public void StartGame() {
        deck = deck.ShuffleNewDeck();
    }

    public void NextPlayer() {

    }

    public override void DetermineWinner() {

    }

    public void Hit(string username) {
        Card card = deck.DrawCard();

    }

    public void SetStatus(GameState status) {
        Status = status;
    }

    private void DealStartingCards() {
        foreach (var player in Players) {
            PlayerHands[player.Username] = new Hand() {
                deck.DrawCard(),
                deck.DrawCard()
            };
        }
    }
}
