namespace ChefConnectMobileApp.Models;

public record ReservationOpinionDTO
{
    public int ReservationId { get; init; }
    public int Rate { get; init; }
    public string Description { get; init; }
}