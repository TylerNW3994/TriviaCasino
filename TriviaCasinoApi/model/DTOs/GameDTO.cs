namespace TriviaCasinoAPI.Model;
public class GameDTO {
    public GameType GameType;
    public DealerDTO? DealerDTO;
    public required List<PlayerDTO> PlayerDTOs;
}
