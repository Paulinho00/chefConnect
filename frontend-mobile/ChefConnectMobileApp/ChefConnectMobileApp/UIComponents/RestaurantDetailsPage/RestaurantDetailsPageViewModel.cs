using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChefConnectMobileApp.UIComponents.RestaurantDetailsPage
{
    public partial class RestaurantDetailsPageViewModel : ObservableObject
    {
        private IRestaurantService _restaurantService = ServiceHelper.GetService<IRestaurantService>();

        [ObservableProperty]
        private Restaurant _restaurant;

        [ObservableProperty]
        private int _rating;

        public RestaurantDetailsPageViewModel(Restaurant restaurant)
        {
            Restaurant = restaurant;
            Init().Wait();
        }

        public async Task Init()
        {
            Rating = await _restaurantService.GetRatingOfRestaurant(_restaurant.Id);
        }
    }
}
