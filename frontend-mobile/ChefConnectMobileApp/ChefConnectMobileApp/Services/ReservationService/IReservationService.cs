using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.ReservationService;

internal interface IReservationService
{
    Task<List<(TimeSpan, int)>> GetTimeSlotsWithAvailableTables(Guid restaurantId, DateTime date);
    Task<Result> MakeReservation(Guid restaurantId, DateTime date, int numberOfTables);
    Task<List<Reservation>> GetReservations();
    Task<Result> CancelReservation(Guid reservationId);
    Task<Result> SendOpinion(ReservationOpinionDTO opinion);
    Task<int> GetRatingOfRestaurant(Guid restaurantId);
}