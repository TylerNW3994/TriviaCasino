using System.ComponentModel.DataAnnotations;
using TriviaCasino.Model;

public class GameActionRequest {
    [Required]
    public required BlackjackGame GameState { get; set; } = new();
    [Required]
    public required string Username { get; set; } = string.Empty;
}
