using System.Text.Json.Serialization;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GameType
{
    Blackjack,
    Poker,
    Slots
}
