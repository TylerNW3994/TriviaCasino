using System.ComponentModel.DataAnnotations;
using TriviaCasino.Model;

public class GameActionRequest {
    [Required]
    public BlackjackGame GameState { get; set; } = new();
    [Required]
    public string Username { get; set; } = string.Empty;
}
