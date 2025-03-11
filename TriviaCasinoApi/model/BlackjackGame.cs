namespace TriviaCasinoAPI.Model;
public class BlackjackGame : ACardGame {
    public List<Card> DealerHand { get; set; } = new();
    public Dictionary<string, int> PlayerScores { get; set; } = new();
    private Dictionary<string, List<Card>> PlayerSplitHands { get; set; } = new();

    public void Initialize() {
        Deck.deckType = DeckType.STANDARD;
    }

    public void StartGame() {
        Deck = Deck.ShuffleNewDeck();
    }

    public void NextPlayer() {
        Player? nextPlayer = GetNextPlayer();

        if (nextPlayer == null) {
            DetermineWinner();
        }
    }

    public override void DetermineWinner() {
        // Each player can win against the dealer
        // Players do NOT compete with each other in Blackjack
        // As such, each player needs to beat the dealer and not bust to be considered a winner
        // If a player busts, they lose, regardless of Dealer score.
        // The Dealer does not reveal their cards if all players bust.
    }

    public void Hit(string username) {
        Card card = Deck.DrawCard();

        if (PlayerHands.TryGetValue(username, out List<Card>? playerHand)) {
            playerHand.Add(card);
            DetermineScore(username, playerHand);
        }
        else {
            throw new InvalidOperationException("Hand does not exist for player " + username);
        }
    }

    public void Stand() {
        NextPlayer();
    }

    public void SetStatus(GameState status) {
        Status = status;
    }

    public override void DealStartingCards() {
        foreach (var player in Players) {
            List<Card> hand = [Deck.DrawCard(), Deck.DrawCard()];
            PlayerHands[player.Username] = hand;
            DetermineScore(player.Username);
        }
    }

    public BlackjackGameDto ToDto() {
        return new BlackjackGameDto {
            DealerHand = DealerHand,
            Deck = Deck,
            PlayerHands = PlayerHands,
            PlayerScores = PlayerScores,
            Players = Players.Select(player => player.ToDto()).ToList(),
            CurrentPlayer = CurrentPlayer
        };
    }

    private void DetermineScore(string username, List<Card> playerHand) {
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

        PlayerScores[username] = score;
    }

    private void DetermineScore(string username) {
        if (PlayerHands.TryGetValue(username, out List<Card>? playerHand)) {
            DetermineScore(username, playerHand);
        } else {
            throw new InvalidOperationException("Hand does not exist for player " + username);
        }
    }

    private readonly int BLACKJACK_MAX_SCORE = 21, ACE_SUBTRACTOR = 10;
}
