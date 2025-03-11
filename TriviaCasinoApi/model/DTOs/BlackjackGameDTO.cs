namespace TriviaCasinoAPI.Model {
    public class BlackjackGameDto {
        public required List<Card> DealerHand { get; set; }
        public required Deck Deck { get; set; }
        public required Dictionary<string, List<Card>> PlayerHands { get; set; }
        public required Dictionary<string, int> PlayerScores { get; set; } 
        public required List<Player> Players { set; get; }
        public required string CurrentPlayer { get; set; }
    }
}
