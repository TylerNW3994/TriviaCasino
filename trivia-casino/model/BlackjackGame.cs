namespace TriviaCasino.Model;
public class BlackjackGame : ACardGame {
    Hand dealerHand = new();
    Dictionary<string, int> playerScores = new();

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
        DetermineScore(username);
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

    private void DetermineScore(string username) {
        if (playerHands.TryGetValue(username, out Hand? playerHand)) {
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
        else
        {
            throw new InvalidOperationException("Hand does not exist for player " + username);
        }
    }
}
