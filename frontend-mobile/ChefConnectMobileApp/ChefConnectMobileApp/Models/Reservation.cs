
using System.Text.Json.Serialization;

namespace ChefConnectMobileApp.Models;

public class Reservation
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    [JsonPropertyName("address")]
    public string Address { get; init; }
    [JsonPropertyName("numberOfTable")]
    public int NumberOfTable { get; init; }
    [JsonPropertyName("date")]
    public DateTime Date { get; init; }
    [JsonPropertyName("reservationStatus")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ReservationStatus Status { get; set; }
    [JsonPropertyName("isOpinion")]
    public bool HasOpinion { get; set; }
}

public enum ReservationStatus
{
    Unconfirmed,
    Confirmed,
    Cancelled
}