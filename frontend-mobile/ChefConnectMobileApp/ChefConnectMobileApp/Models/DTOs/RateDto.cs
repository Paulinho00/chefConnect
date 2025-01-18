using System.Text.Json.Serialization;

namespace ChefConnectMobileApp.Models;

public class RateDto
{
    [JsonPropertyName("avarageRate")]
    public int Rate { get; init; }
}