namespace TriviaCasinoAPI.Model;
public class BlackjackGame : ACardGame {
    public List<Card> DealerHand { get; set; } = new();
    public int DealerScore { get; set; }
    public Dictionary<string, PlayerData> PlayerDatas { get; set; } = new();

    public BlackjackGame(string gameId) {
        GameId = gameId;
    }

    public void Initialize() {
        Deck.deckType = DeckType.STANDARD;
    }

    public void StartGame() {
        Deck = Deck.ShuffleNewDeck();
        DealerHand.Clear();
        DealerHand.AddRange(Deck.DrawCards(2));
        DealerScore = DetermineScore(DealerHand);
        
        PlayerDatas.Clear();
        foreach (var player in Players) {
            PlayerData data = new();
            data.Score = 0;
            data.Status = STATUS_IN_PLAY;

            PlayerDatas.Add(player.Username, data);
        }

        DealStartingCards();
    }

    public void NextPlayer() {
        Player? nextPlayer = GetNextPlayer();

        if (nextPlayer == null) {
            DetermineWinner();
        }
    }

    public override void DetermineWinner() {
        // Players already lost, no need to determine Dealer information
        if (playersBusted == Players.Count) {
            return;
        }

        DealerScore = DetermineScore(DealerHand);

        while (DealerScore < DEALER_CUTOFF) {
            DealerHand.Add(Deck.DrawCard());
            DealerScore = DetermineScore(DealerHand);
        }

        if (DealerScore > BLACKJACK_MAX_SCORE) {
            foreach (var player in PlayerDatas) {
                PlayerData data = player.Value;
                if (data.Status != STATUS_BUST) {
                    data.Status = STATUS_WIN;
                }
            }
        } else {
            foreach (var player in PlayerDatas) {
                PlayerData data = player.Value;
                if (data.Status != STATUS_IN_PLAY) {
                    continue;
                }

                int playerScore = data.Score;
                if (playerScore > DealerScore) {
                    data.Status = STATUS_WIN;
                } else if (playerScore == DealerScore) {
                    data.Status = STATUS_TIE;
                } else {
                    data.Status = STATUS_LOSE;
                }
            }
        }
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
            List<Card> hand = Deck.DrawCards(2);
            PlayerHands[player.Username] = hand;
            DetermineScore(player.Username);
        }
    }

    public BlackjackGameDto ToDto() {
        return new BlackjackGameDto {
            GameId = GameId,
            DealerHand = DealerHand.Select(card => new Card (
                card.Value,
                card.Rank,
                card.Suit
            )).ToList(),
            DealerScore = DealerScore,
            Deck = Deck,
            PlayerHands = PlayerHands.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Select(card => new Card (
                    card.Value,
                    card.Rank,
                    card.Suit
                )).ToList()
            ),
            PlayerDatas = PlayerDatas,
            Players = Players.Select(player => player.ToDto()).ToList(),
            CurrentPlayer = CurrentPlayer
        };
    }

    internal void DetermineScore(string username, List<Card> playerHand) {
        int score = DetermineScore(playerHand);
        PlayerDatas[username].Score = score;
        bool playerBusted = score > BLACKJACK_MAX_SCORE, playerDrewBlackjack = score == BLACKJACK_MAX_SCORE && PlayerHands[username].Count == 2; 

        if (playerBusted) {
            playersBusted++;
            PlayerDatas[username].Status = STATUS_BUST;
            NextPlayer();
            return;
        } else if (playerDrewBlackjack) {
            PlayerDatas[username].Status = STATUS_BLACKJACK;
            NextPlayer();
            return;
        }

        PlayerDatas[username].Status = STATUS_IN_PLAY;
    }

    internal void DetermineScore(string username) {
        if (PlayerHands.TryGetValue(username, out List<Card>? playerHand)) {
            DetermineScore(username, playerHand);
        } else {
            throw new InvalidOperationException("Hand does not exist for player " + username);
        }
    }

    internal int DetermineScore(List<Card> hand) {
        int score = 0, aceCount = 0;
        foreach (Card card in hand) {
            score += card.Value;

            if (card.Rank == "A") {
                aceCount++;
            }
        }

        //In Blackjack, Aces can count for either a 1 or 11.  If the player's score goes over 21, subtract 10 from that score if they have an Ace to count it as a 1.
        while (score > BLACKJACK_MAX_SCORE && aceCount > 0) {
            score -= ACE_SUBTRACTOR;
            aceCount--;
        }

        return score;
    }

    public class PlayerData {
        public int Score { get; set; }
        public int Status { get; set; }
    }

    private int playersBusted = 0;

    private readonly int BLACKJACK_MAX_SCORE = 21, ACE_SUBTRACTOR = 10, DEALER_CUTOFF = 17;
    internal readonly int STATUS_BUST = -1, STATUS_LOSE = -1, STATUS_IN_PLAY = 0, STATUS_TIE = 0, STATUS_WIN = 1, STATUS_BLACKJACK = 2;
}
