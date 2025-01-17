namespace ChefConnectMobileApp.Models;

public record ReservationOpinionDTO
{
    public Guid ReservationId { get; init; }
    public int Rate { get; init; }
    public string Description { get; init; }
}