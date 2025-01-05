
using System.Text.Json.Serialization;

namespace ChefConnectMobileApp.Models;

public record Restaurant
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    [JsonPropertyName("numberOfSeats")]
    public int NumberOfSeats { get; init; }
    [JsonPropertyName("address")]
    public string Address { get; init; }
    [JsonPropertyName("name")]

    public string Name { get; init; }
    [JsonPropertyName("openTime")]
    public TimeSpan OpenTime { get; init; }
    [JsonPropertyName("closeTime")]
    public TimeSpan CloseTime { get; init; }
};