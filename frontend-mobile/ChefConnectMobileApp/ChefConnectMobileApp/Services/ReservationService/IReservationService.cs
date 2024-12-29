using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.ReservationService;

internal interface IReservationService
{
    Task<List<(TimeSpan, int)>> GetTimeSlotsWithAvailableTables(int restaurantId, DateTime date);
    Task<Result<string>> MakeReservation(int restaurantId, DateTime date, int numberOfTables);
    Task<List<Reservation>> GetReservations();
}