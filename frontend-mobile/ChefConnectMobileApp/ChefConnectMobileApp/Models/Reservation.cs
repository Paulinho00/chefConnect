
namespace ChefConnectMobileApp.Models;

public class Reservation
{
    public int Id { get; init; }
    public string Address { get; init; }
    public int NumberOfTable { get; init; }
    public DateTime Date { get; init; }
    //TODO: Change set to init after calls to API are implemented
    public ReservationStatus Status { get; set; }
}

public enum ReservationStatus
{
    Unconfirmed,
    Confirmed,
    Cancelled,
    OpinionSaved
}