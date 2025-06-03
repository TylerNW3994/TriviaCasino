namespace TriviaCasinoAPI.Model;
public abstract class AGameDTO {
    public required string GameId { get; set; }
    public required string CurrentPlayer { get; set; }
    public required string Message { get; set; }
}
