using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.RestaurantDetailsPage
{
    public partial class RestaurantDetailsPageViewModel : ObservableObject
    {
        private IRestaurantService _restaurantService = ServiceHelper.GetService<IRestaurantService>();

        [ObservableProperty]
        private Restaurant _restaurant;

        [ObservableProperty]
        private int _rating;

        [ObservableProperty] 
        private bool _isReservationsVisible = false;

        [ObservableProperty]
        private DateTime _minimumDate = DateTime.Now; 

        partial void OnRestaurantChanged(Restaurant value)
        {
            UpdateRating().Wait();
        }

        private async Task UpdateRating()
        {
            Rating = await _restaurantService.GetRatingOfRestaurant(_restaurant.Id);
        }

        [RelayCommand]
        private async Task ChangeReservationsVisibility()
        {
            IsReservationsVisible = !IsReservationsVisible;
        }
    }
}
