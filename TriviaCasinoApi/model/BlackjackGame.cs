namespace TriviaCasinoAPI.Model;

public class BlackjackGame : ACardGame {
    public List<Card> DealerHand { get; set; } = [];
    public int DealerScore { get; set; }
    public Dictionary<string, PlayerBlackjackData> PlayerDatas { get; set; } = [];

    public BlackjackGame(string gameId) {
        GameId = gameId;
    }

    public override void Initialize() {
        Deck.deckType = DeckType.STANDARD;
    }

    public override void StartGame() {
        Deck = Deck.ShuffleNewDeck();
        DealerHand.Clear();
        DealerHand.AddRange(Deck.DrawCards(2));
        DealerScore = DetermineScore(DealerHand);
        playersBusted = 0;
        Message = "New Game Started.";

        PlayerDatas.Clear();
        foreach (var player in Players) {
            PlayerBlackjackData data = new() {
                Score = 0,
                Status = STATUS_IN_PLAY,
                Chips = player.Chips,
                Bet = player.Bet
            };

            PlayerDatas.Add(player.Username, data);
        }

        DealStartingCards();
    }

    public void NextPlayer()
    {
        Player? nextPlayer = GetNextPlayer();

        if (nextPlayer == null)
        {
            DetermineWinner();
            AdjustChips(PlayerDatas);
        }
    }

    public override void DetermineWinner()
    {
        // Players already lost, no need to determine Dealer information
        if (playersBusted == Players.Count)
        {
            return;
        }

        DealerScore = DetermineScore(DealerHand);

        while (DealerScore < DEALER_CUTOFF)
        {
            DealerHand.Add(Deck.DrawCard());
            DealerScore = DetermineScore(DealerHand);
        }

        if (DealerScore > BLACKJACK_MAX_SCORE) {
            foreach (var player in PlayerDatas) {
                PlayerBlackjackData data = player.Value;
                if (data.Status != STATUS_BUST) {
                    data.Status = STATUS_WIN;
                }
            }
        }
        else {
            foreach (var player in PlayerDatas) {
                PlayerBlackjackData data = player.Value;
                if (data.Status != STATUS_IN_PLAY) {
                    continue;
                }

                int playerScore = data.Score;
                if (playerScore > DealerScore) {
                    data.Status = STATUS_WIN;
                }
                else if (playerScore == DealerScore) {
                    data.Status = STATUS_TIE;
                }
                else {
                    data.Status = STATUS_LOSE;
                }
            }
        }

        foreach (var player in Players) {
            player.BetMultiplier = PlayerDatas[player.Username].Status;
        }
    }

    public void Hit(string username)
    {
        Card card = Deck.DrawCard();

        if (PlayerDatas.TryGetValue(username, out PlayerBlackjackData? data))
        {
            if (data.Hand == null)
            {
                throw new InvalidOperationException("Hand does not exist for player " + username);
            }
            data.Hand.Add(card);
            DetermineScore(username, data.Hand);
        }
        else
        {
            throw new InvalidOperationException("Hand does not exist for player " + username);
        }
    }

    public void Stand()
    {
        NextPlayer();
    }

    public void SetState(GameState state) {
        State = state;
    }

    public override void DealStartingCards() {
        foreach (var player in Players) {
            List<Card> hand = Deck.DrawCards(2);
            PlayerDatas[player.Username].Hand = hand;
            PlayerDatas[player.Username].Score = DetermineScore(hand);
        }
    }

    public override BlackjackGameDTO ToDto() {
        return new BlackjackGameDTO {
            GameId = GameId,
            DealerHand = DealerHand.Select(card => new Card(
                card.Value,
                card.Rank,
                card.Suit
            )).ToList(),
            DealerScore = DealerScore,
            Deck = Deck,
            PlayerDatas = PlayerDatas,
            CurrentPlayer = CurrentPlayer,
            Message = Message
        };
    }

    public override GameDTO ToApiResponseDto() {
        return new GameDTO {
            GameType = GameType.Blackjack,
            DealerDTO = new DealerDTO{ Hand = DealerHand, Score = DealerScore },
            PlayerDTOs = Players.Select(player => player.ToPlayerCardGameDto(PlayerDatas[player.Username])).Cast<APlayerDTO>().ToList()
        };
    }

    internal void DetermineScore(string username, List<Card> playerHand) {
        int score = DetermineScore(playerHand);
        PlayerDatas[username].Score = score;
        bool playerBusted = score > BLACKJACK_MAX_SCORE,
            playerDrewBlackjack = score == BLACKJACK_MAX_SCORE && PlayerDatas[username]?.Hand?.Count == 2;

        if (playerBusted) {
            playersBusted++;
            PlayerDatas[username].Status = STATUS_BUST;
            NextPlayer();
            return;
        }
        else if (playerDrewBlackjack) {
            PlayerDatas[username].Status = STATUS_BLACKJACK;
            NextPlayer();
            return;
        }

        PlayerDatas[username].Status = STATUS_IN_PLAY;
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

    private int playersBusted = 0;
    private string Message = "";

    private readonly int BLACKJACK_MAX_SCORE = 21, ACE_SUBTRACTOR = 10, DEALER_CUTOFF = 17;
    internal readonly double STATUS_BUST = -1, STATUS_LOSE = -1, STATUS_IN_PLAY = 0, STATUS_TIE = 0, STATUS_WIN = 1, STATUS_BLACKJACK = 1.5;
}
