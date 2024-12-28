using AsyncAwaitBestPractices;
using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services;
using ChefConnectMobileApp.UIComponents.RestaurantListElement;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChefConnectMobileApp.UIComponents.RestaurantsListPage;

public partial class RestaurantListPageViewModel : ObservableObject
{
    private IRestaurantService _restaurantService = ServiceHelper.GetService<IRestaurantService>();

    public RestaurantListPageViewModel()
    {
        Init().Wait();
    }

    public async Task Init()
    {
        var restaurants = await _restaurantService.GetAllRestaurants();
        var restaurantViewModels = new List<RestaurantListElementViewModel>();
        foreach (var restaurant in restaurants)
        {
            var restaurantViewModel = new RestaurantListElementViewModel(restaurant);
            restaurantViewModels.Add(restaurantViewModel);
        }

        _restaurants = restaurantViewModels;
    }
    
    [ObservableProperty]
    private List<RestaurantListElementViewModel> _restaurants;
    
}