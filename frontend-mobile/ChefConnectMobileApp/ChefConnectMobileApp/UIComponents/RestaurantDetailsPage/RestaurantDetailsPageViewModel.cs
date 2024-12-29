using Android.Text.Format;
using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services;
using ChefConnectMobileApp.Services.ReservationService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Platform;

namespace ChefConnectMobileApp.UIComponents.RestaurantDetailsPage
{
    public partial class RestaurantDetailsPageViewModel : ObservableObject
    {
        private IRestaurantService _restaurantService = ServiceHelper.GetService<IRestaurantService>();
        private IReservationService _reservationService = ServiceHelper.GetService<IReservationService>();

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

        partial void OnRestaurantChanged(Restaurant value)
        {
            UpdateRating().Wait();
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

        partial void OnSelectedDateChanged(DateTime value)
        {
            GetTimeSlotsWithAvailableTables().Wait();
            TimeSlots = _timeSlotsWithNumberOfAvailableTables
                .Where(slot => slot.NumberOfFreeTables > 0)
                .Select(slot => slot.TimeSlot.ToString(@"hh\:mm"))
                .ToList();
            SelectedTimeSlot = TimeSlots[0];
        }

        private async Task GetTimeSlotsWithAvailableTables()
        {
            _timeSlotsWithNumberOfAvailableTables =
                await _reservationService.GetTimeSlotsWithAvailableTables(Restaurant.Id, SelectedDate);
        }

        private async Task UpdateRating()
        {
            Rating = await _restaurantService.GetRatingOfRestaurant(Restaurant.Id);
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
    }
}
