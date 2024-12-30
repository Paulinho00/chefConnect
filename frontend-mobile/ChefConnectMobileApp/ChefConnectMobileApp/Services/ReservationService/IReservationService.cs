using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.ReservationService;

internal interface IReservationService
{
    Task<Result<List<(TimeSpan, int)>>> GetTimeSlotsWithAvailableTables(int restaurantId, DateTime date);
    Task<Result> MakeReservation(int restaurantId, DateTime date, int numberOfTables);
    Task<Result<List<Reservation>>> GetReservations();
    Task<Result> CancelReservation(int reservationId);
    Task<Result> SendOpinion(ReservationOpinionDTO opinion);
}