using System.Text.Json.Serialization;

namespace ChefConnectMobileApp.Models;

public record ReservationOpinionDTO
{
    [JsonPropertyName("reservationId")]
    public Guid ReservationId { get; init; }
    [JsonPropertyName("rate")]
    public int Rate { get; init; }
    [JsonPropertyName("description")]
    public string Description { get; init; }
}