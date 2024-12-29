namespace ChefConnectMobileApp.Services.ReservationService;

public class ReservationService : IReservationService
{
    private List<(TimeSpan, int)> _mockSlots = new List<(TimeSpan, int)>
    {
        (new TimeSpan(10, 0, 0), 5),
        (new TimeSpan(11, 00, 0), 3),
        (new TimeSpan(12, 00, 0), 1),
        (new TimeSpan(13, 00, 0), 0),
        (new TimeSpan(14, 00, 0), 10),
    };

    public async Task<List<(TimeSpan, int)>> GetTimeSlotsWithAvailableTables(int restaurantId, DateTime date)
    {
        //TODO: Call to API
        return _mockSlots;
    }
}