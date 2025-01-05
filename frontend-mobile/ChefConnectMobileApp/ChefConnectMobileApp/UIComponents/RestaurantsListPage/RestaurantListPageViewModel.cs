using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantListElementViewModel = ChefConnectMobileApp.UIComponents.RestaurantListElementView.RestaurantListElementViewModel;

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
        var restaurants = await _restaurantService.GetAllRestaurants().ConfigureAwait(false);
        var restaurantViewModels = new List<RestaurantListElementViewModel>();
        foreach (var restaurant in restaurants)
        {
            var restaurantViewModel = new RestaurantListElementViewModel(restaurant);
            restaurantViewModels.Add(restaurantViewModel);
        }

        Restaurants = restaurantViewModels;
    }
    
    [ObservableProperty]
    private List<RestaurantListElementViewModel> _restaurants;
    
}