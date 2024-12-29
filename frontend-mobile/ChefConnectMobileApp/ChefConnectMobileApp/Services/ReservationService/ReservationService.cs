﻿using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.AuthService;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.ReservationService;

internal class ReservationService : IReservationService
{
    public async Task<List<(TimeSpan, int)>> GetTimeSlotsWithAvailableTables(int restaurantId, DateTime date)
    {
        //TODO: Call to API
        return _mockSlots;
    }

    public async Task<Result<string>> MakeReservation(int restaurantId, DateTime date, int numberOfTables)
    {
        //TODO: Call to API
        //User ID will be in token
        return new Result<string>();
    }

    public async Task<List<Reservation>> GetReservations()
    {
        //TODO: Call to API
        return _reservations;
    }

    public async Task<Result<string>> CancelReservation(int reservationId)
    {
        //TODO: Call to API
        _reservations.FirstOrDefault(x => x.Id == reservationId).Status = ReservationStatus.Cancelled;
        return new Result<string>();
    }

    public async Task<Result<string>> SendOpinion(ReservationOpinionDTO opinion)
    {
        //TODO: Call to API
        return new Result<string>();
    }

    private List<(TimeSpan, int)> _mockSlots = new List<(TimeSpan, int)>
    {
        (new TimeSpan(10, 0, 0), 5),
        (new TimeSpan(11, 00, 0), 3),
        (new TimeSpan(12, 00, 0), 1),
        (new TimeSpan(13, 00, 0), 0),
        (new TimeSpan(14, 00, 0), 10),
    };

    private List<Reservation> _reservations = new List<Reservation>()
    {
        new Reservation
        {
            Address = "ul. XD 1 50-501 Wrocław",
            Date = new DateTime(new DateOnly(2025, 1, 2), new TimeOnly(13, 30)),
            Id = 1,
            NumberOfTable = 2,
            Status = ReservationStatus.Unconfirmed
        },
        new Reservation
        {
            Address = "ul. Beka 30 50-501 Wrocław",
            Date = new DateTime(new DateOnly(2025, 2, 2), new TimeOnly(14, 0)),
            Id = 2,
            NumberOfTable = 4,
            Status = ReservationStatus.Confirmed
        },
        new Reservation
        {
            Address = "ul. Siema 30 50-501 Wrocław",
            Date = new DateTime(new DateOnly(2025, 1, 15), new TimeOnly(12, 0)),
            Id = 3,
            NumberOfTable = 9,
            Status = ReservationStatus.Cancelled
        },
        new Reservation
        {
            Address = "ul. Nie działa 14 50-501 Wrocław",
            Date = new DateTime(new DateOnly(2024, 12, 29), new TimeOnly(13, 0)),
            Id = 4,
            NumberOfTable = 9,
            Status = ReservationStatus.Confirmed
        }
    };
}