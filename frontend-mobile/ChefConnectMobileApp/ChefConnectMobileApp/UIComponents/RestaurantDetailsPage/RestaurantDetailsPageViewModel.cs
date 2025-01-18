using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.ReservationService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;

namespace ChefConnectMobileApp.UIComponents.RestaurantDetailsPage;

public partial class RestaurantDetailsPageViewModel : ObservableObject
{
    private IRestaurantService _restaurantService = ServiceHelper.GetService<IRestaurantService>();
    private IReservationService _reservationService = ServiceHelper.GetService<IReservationService>();
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();

    [ObservableProperty]
    private Restaurant _restaurant;

    [ObservableProperty]
    private int _rating;

    [ObservableProperty] 
    private bool _isReservationsVisible = false;

    [ObservableProperty]
    private DateTime _minimumDate = DateTime.Now;

    [ObservableProperty]
    private DateTime _selectedDate;

    [ObservableProperty]
    private List<string> _timeSlots;

    [ObservableProperty]
    private string _selectedTimeSlot;

    [ObservableProperty]
    private int _numberOfFreeTablesForTimeSlot;

    [ObservableProperty]
    private bool _isNumberOfTablesInputValid;

    [ObservableProperty] 
    private int _selectedNumberOfTables = 0;

    private List<(TimeSpan TimeSlot, int NumberOfFreeTables)> _timeSlotsWithNumberOfAvailableTables;

    async partial void OnRestaurantChanged(Restaurant value)
    {
        await UpdateRating().ConfigureAwait(false);
    }

    partial void OnSelectedNumberOfTablesChanged(int value)
    { 
        IsNumberOfTablesInputValid = SelectedNumberOfTables > 0 && SelectedNumberOfTables <= NumberOfFreeTablesForTimeSlot;
    }

    partial void OnSelectedTimeSlotChanged(string value)
    {
        var timespan = TimeSpan.Parse(SelectedTimeSlot);
        NumberOfFreeTablesForTimeSlot = _timeSlotsWithNumberOfAvailableTables.First(x => x.Item1 == timespan).NumberOfFreeTables;
    }

    async partial void OnSelectedDateChanged(DateTime value)
    {
        await GetTimeSlotsWithAvailableTables().ConfigureAwait(false);
        TimeSlots = _timeSlotsWithNumberOfAvailableTables
            .Where(slot => slot.NumberOfFreeTables > 0)
            .Select(slot => slot.TimeSlot.ToString(@"hh\:mm"))
            .ToList();
        SelectedTimeSlot = TimeSlots[0];
    }

    private async Task GetTimeSlotsWithAvailableTables()
    {
        _timeSlotsWithNumberOfAvailableTables =
            await _reservationService.GetTimeSlotsWithAvailableTables(Restaurant.Id, SelectedDate.Date).ConfigureAwait(false);
    }

    private async Task UpdateRating()
    {
        Rating = await _reservationService.GetRatingOfRestaurant(Restaurant.Id).ConfigureAwait(false);
    }

    [RelayCommand]
    private async Task ChangeReservationsVisibility()
    {
        IsReservationsVisible = !IsReservationsVisible;
        if (IsReservationsVisible)
        {
            SelectedDate = MinimumDate;
        }
    }

    [RelayCommand]
    private async Task MakeReservation()
    {
        var date = SelectedDate + TimeSpan.Parse(SelectedTimeSlot);
        var result = await _reservationService.MakeReservation(Restaurant.Id, date, SelectedNumberOfTables).ConfigureAwait(false);
        if (result.IsFailure)
        {
            _alertService.ShowAlert("Rezerwacja nieudana", result.Error);
        }
        else
        {
            IsReservationsVisible = false;
            _alertService.ShowAlert("Rezerwacja udana",
                "Twoja rezerwacja została zapisana. Sprawdź jej status, czy została zaakceptowana przez naszego pracownika");
        }
    }
}