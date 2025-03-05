namespace TriviaCasino.Model;
public class BlackjackGame : ACardGame {
    Hand dealerHand = new();
    Dictionary<string, int> playerScores = new();
    private Dictionary<string, Hand> playerSplitHands = new();

    public void Initialize() {
        deck.deckType = DeckType.STANDARD;
    }

    public void StartGame() {
        deck = deck.ShuffleNewDeck();
    }

    public void NextPlayer() {
        // Go through all the players
        // When you get to the end of the list,
        // go to DetemineWinner
    }

    public override void DetermineWinner() {
        // Each player can win against the dealer
        // Players do NOT compete with each other in Blackjack
        // As such, each player needs to beat the dealer and not bust to be considered a winner
        // If a player busts, they lose, regardless of Dealer score.
        // The Dealer does not reveal their cards if all players bust.
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

    public void Stand() {
        // Could implement NextPlayer here instead.
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
        while (score > BLACKJACK_MAX_SCORE && aceCount > 0) {
            score -= ACE_SUBTRACTOR;
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

    private readonly int BLACKJACK_MAX_SCORE = 21, ACE_SUBTRACTOR = 10;
}
