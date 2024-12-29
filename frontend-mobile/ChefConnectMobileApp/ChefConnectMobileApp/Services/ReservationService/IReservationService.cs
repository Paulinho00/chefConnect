namespace ChefConnectMobileApp.Services.ReservationService;

public interface IReservationService
{
    Task<List<(TimeSpan, int)>> GetTimeSlotsWithAvailableTables(int restaurantId, DateTime date);
}