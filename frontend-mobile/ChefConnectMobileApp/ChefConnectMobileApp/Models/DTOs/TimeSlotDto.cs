using System.Text.Json.Serialization;

namespace ChefConnectMobileApp.Models;

public class TimeSlotDto
{
    [JsonPropertyName("timeSpan")]
    public TimeSpan Time { get; init; }
    [JsonPropertyName("value")]
    public int NumberOfFreePlaces { get; init; }
}