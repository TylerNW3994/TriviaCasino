namespace TriviaCasinoApi.Model;

public class GameDTO {
    public string GameId;
    public GameType GameType;
    public DealerDTO? DealerDTO;
    public required List<APlayerDTO> PlayerDTOs;
}
