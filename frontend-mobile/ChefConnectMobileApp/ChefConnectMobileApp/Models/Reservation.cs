
namespace ChefConnectMobileApp.Models;

public class Reservation
{
    public Guid Id { get; init; }
    public string Address { get; init; }
    public int NumberOfTable { get; init; }
    public DateTime Date { get; init; }
    public ReservationStatus Status { get; set; }
}

public enum ReservationStatus
{
    Unconfirmed,
    Confirmed,
    Cancelled,
    OpinionSaved
}