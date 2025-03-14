using TriviaCasinoApi.Controllers;
using TriviaCasinoAPI.Model;
namespace TriviaCasino.Tests;

public class BlackjackGameTest {
    private string gameId;
    private BlackjackGame game;
    public BlackjackGameTest() {
        gameId = Guid.NewGuid().ToString();
        game = new(gameId);
    }

    [Fact]
    public void GameShouldInitialize() {
        Assert.NotNull(game);

        game.Initialize();

        Assert.Equal(DeckType.STANDARD, game.Deck.deckType);
    }

    [Fact]
    public void ShouldDealTwoCardsToPlayerAndDealer() {
        Player player = new();
        player.Username = "TestPlayer";

        game.Players.Add(player);

        game.StartGame();

        Assert.Equal(2, game.PlayerHands[player.Username].Count);
        Assert.Equal(2, game.DealerHand.Count);
    }

    [Fact]
    public void ShouldDetermineScoreCorrectly() {
        List<Card> hand = [
            new Card(2, "2", "Spade"),
            new Card(2, "2", "Diamond")
        ];

        Assert.Equal(4, game.DetermineScore(hand));
    }

    [Fact]
    public void ShouldDetermineAceValueCorrectly() {
        List<Card> hand = [
            new Card(11, "A", "Spade"),
            new Card(11, "A", "Diamond")
        ];

        Assert.Equal(12, game.DetermineScore(hand));
    }

    [Fact]
    public void ShouldDeterminePlayerBust() {
        Player player = new();
        player.Username = "TestPlayer";

        game.Players.Add(player);
        game.StartGame();

        List<Card> hand = [
            new Card(10, "10", "Spade"),
            new Card(10, "10", "Diamond"),
            new Card(10, "10", "Heart")
        ];

        game.PlayerHands[player.Username] = hand;

        game.DetermineScore(player.Username);

        Assert.Equal(game.PlayerDatas[player.Username].Status, game.STATUS_BUST);
    }

    [Fact]
    public void ShouldDeterminePlayerWinnersAndLosers() {
        Player bustPlayer = new(), winnerPlayer = new(), loserPlayer = new(), tiedPlayer = new(), blackjackedPlayer = new();
        bustPlayer.Username = "BustPlayer";
        winnerPlayer.Username = "WinnerPlayer";
        loserPlayer.Username = "LoserPlayer";
        tiedPlayer.Username = "TiedPlayer";
        blackjackedPlayer.Username = "BlackjackedPlayer";

        game.Players.AddRange([bustPlayer, winnerPlayer, loserPlayer, tiedPlayer, blackjackedPlayer]);
        game.StartGame();

        List<Card> bustHand = [
            new Card(10, "10", "Spade"),
            new Card(10, "10", "Diamond"),
            new Card(10, "10", "Heart")
        ];

        game.PlayerHands[bustPlayer.Username] = bustHand;
        game.DetermineScore(bustPlayer.Username);

        List<Card> winHand = [
            new Card(10, "10", "Spade"),
            new Card(10, "10", "Diamond"),
        ];

        game.PlayerHands[winnerPlayer.Username] = winHand;
        game.DetermineScore(winnerPlayer.Username);

        List<Card> loserHand = [
            new Card(2, "2", "Spade"),
            new Card(2, "2", "Diamond"),
        ];

        game.PlayerHands[loserPlayer.Username] = loserHand;
        game.DetermineScore(loserPlayer.Username);

        List<Card> tieHand = [
            new Card(10, "10", "Spade"),
            new Card(7, "7", "Diamond")
        ];

        game.PlayerHands[tiedPlayer.Username] = tieHand;
        game.DetermineScore(tiedPlayer.Username);

        List<Card> blackjackedHand = [
            new Card(10, "10", "Spade"),
            new Card(11, "A", "Diamond"),
        ];

        game.PlayerHands[blackjackedPlayer.Username] = blackjackedHand;
        game.DetermineScore(blackjackedPlayer.Username);

        game.DealerHand = new List<Card>{
            new Card(10, "10", "Spade"),
            new Card(7, "7", "Diamond")
        };
        game.DetermineWinner();

        Assert.Equal(game.PlayerDatas[winnerPlayer.Username].Status, game.STATUS_WIN);
        Assert.Equal(game.PlayerDatas[loserPlayer.Username].Status, game.STATUS_LOSE);
        Assert.Equal(game.PlayerDatas[bustPlayer.Username].Status, game.STATUS_BUST);
        Assert.Equal(game.PlayerDatas[winnerPlayer.Username].Status, game.STATUS_WIN);
        Assert.Equal(game.PlayerDatas[blackjackedPlayer.Username].Status, game.STATUS_BLACKJACK);
    }
}
