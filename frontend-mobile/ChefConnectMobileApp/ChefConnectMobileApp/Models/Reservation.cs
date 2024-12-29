
namespace ChefConnectMobileApp.Models;

public class Reservation
{
    public int Id { get; init; }
    public string Address { get; init; }
    public int NumberOfTable { get; init; }
    public DateTime Date { get; init; }
    public ReservationStatus Status { get; init; }
}

public enum ReservationStatus
{
    Unconfirmed,
    Confirmed,
    Cancelled
}