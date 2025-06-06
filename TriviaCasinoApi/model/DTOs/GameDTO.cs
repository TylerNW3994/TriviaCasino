namespace TriviaCasinoApi.Model;

public class GameDTO
{
    public string GameId { get; set; }
    public GameType GameType { get; set; }
    public DealerDTO? DealerDTO { get; set; }
    public List<PlayerCardGameDTO> PlayerDTOs { get; set; }
    public string Message { get; set; } = string.Empty;
    public string CurrentPlayer { get; set; } = string.Empty;
}
