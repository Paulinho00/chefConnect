namespace ChefConnectMobileApp.Models;

public record MakeReservationRequestDto
{
    public Guid RestaurantId { get; set; }
    public int NumberOfSeats { get; set; }
    public DateTime DateTime { get; set; }
}