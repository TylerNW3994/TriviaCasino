namespace TriviaCasinoAPI.Model;
public class BlackjackGameDTO : ACardGameDTO {
    public required List<Card> DealerHand { get; set; }
    public required int DealerScore { get; set; }
    public required Dictionary<string, PlayerBlackjackData> PlayerDatas { get; set; }
}
