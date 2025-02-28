namespace TriviaCasino.Model;
public class BlackjackGame : ACardGame {
    Hand dealerHand = new();
    Dictionary<string, int> playerScores = new();
    private Dictionary<string, Hand> playerSplitHands = new();

    // string currentPlayer = "";

    public void Initialize() {
        deck.deckType = DeckType.STANDARD;
    }

    public void StartGame() {
        deck = deck.ShuffleNewDeck();
    }

    public void NextPlayer() {
        // Go through all the players
        // When you get to the end of the list,
        // Determine the dealer's score,
        // If the dealer busts, all the players win.
        
    }

    public override void DetermineWinner() {

    }

    public void Hit(string username) {
        Card card = deck.DrawCard();

        if (playerHands.TryGetValue(username, out Hand? playerHand)) {
            playerHand.Add(card);
            DetermineScore(username, playerHand);
        }
        else {
            throw new InvalidOperationException("Hand does not exist for player " + username);
        }
    }

    public void SetStatus(GameState status) {
        Status = status;
    }

    public override void DealStartingCards() {
        foreach (var player in Players) {
            playerHands[player.Username] = new Hand() {
                deck.DrawCard(),
                deck.DrawCard()
            };
            DetermineScore(player.Username);
        }
    }

    private void DetermineScore(string username, Hand playerHand) {
            int score = 0, aceCount = 0;
            foreach (Card card in playerHand) {
                score += card.value;

                if (card.rank == "A") {
                    aceCount++;
                }
            }

            //In Blackjack, Aces can count for either a 1 or 11.  If the player's score goes over 21, subtract 10 from that score if they have an Ace to count it as a 1.
            while (score > 21 && aceCount > 0) {
                score -= 10;
                aceCount--;
            }

            playerScores[username] = score;
    }

    private void DetermineScore(string username) {
        if (playerHands.TryGetValue(username, out Hand? playerHand)) {
            DetermineScore(username, playerHand);
        } else {
            throw new InvalidOperationException("Hand does not exist for player " + username);
        }
    }
}
