namespace TriviaCasinoAPI.Model {
    public class BlackjackGameDto {
        public required string GameId { get; set; }
        public required List<Card> DealerHand { get; set; }
        public required int DealerScore { get; set; }
        public required Deck Deck { get; set; }
        public required Dictionary<string, PlayerBlackjackData> PlayerDatas { get; set; }
        public required string CurrentPlayer { get; set; }
    }
}
