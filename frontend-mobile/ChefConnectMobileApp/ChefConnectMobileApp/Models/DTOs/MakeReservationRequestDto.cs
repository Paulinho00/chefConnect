using System.Text.Json.Serialization;

namespace ChefConnectMobileApp.Models;

public record MakeReservationRequestDto
{
    [JsonPropertyName("restaurantId")]
    public Guid RestaurantId { get; set; }
    [JsonPropertyName("numberOfPeople")]
    public int NumberOfSeats { get; set; }
    [JsonPropertyName("date")]
    public string Date { get; set; }
}