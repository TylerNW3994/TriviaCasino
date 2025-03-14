namespace TriviaCasinoAPI.Model {
    public class BlackjackGameDto {
        public required string GameId { get; set; }
        public required List<Card> DealerHand { get; set; }
        public required int DealerScore { get; set; }
        public required Deck Deck { get; set; }
        public required Dictionary<string, List<Card>> PlayerHands { get; set; }
        public required Dictionary<string, BlackjackGame.PlayerData> PlayerDatas { get; set; }
        public required List<PlayerDTO> Players { set; get; }
        public required string CurrentPlayer { get; set; }
    }
}
