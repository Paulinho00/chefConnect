using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.RestaurantListElementView;

public partial class RestaurantListElementViewModel : ObservableObject
{
    private IRestaurantService _restaurantService = ServiceHelper.GetService<IRestaurantService>();
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();
    private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();

    [ObservableProperty] 
    private Restaurant _restaurant;

    [ObservableProperty]
    private int _rating;

    [ObservableProperty]
    private bool _isFavourite;

    public RestaurantListElementViewModel()
    {

    }

    public RestaurantListElementViewModel(Restaurant restaurant)
    {
        _restaurant = restaurant;
        Init().Wait();
    }

    partial void OnIsFavouriteChanged(bool value)
    {
        if (value)
            AddNewFavourite().Wait();
        else
            RemoveFavourite().Wait();
    }

    public async Task Init()
    {
        Rating = await _restaurantService.GetRatingOfRestaurant(_restaurant.Id);
        IsFavourite = await _restaurantService.IsFavouriteForCurrentUser(_restaurant.Id);
    }

    [RelayCommand]
    private async Task GoToRestaurantsDetails()
    {
        await _navigationService.TransitToPageAsync(new RestaurantDetailsPage.RestaurantDetailsPage(Restaurant), false);
    }
    
    public async Task AddNewFavourite()
    {
        var result = await _restaurantService.AddNewFavourite(_restaurant.Id);
        if (result.IsFailure)
        {
            await _alertService.ShowAlertAsync("Błąd", result.Error);
            IsFavourite = false;
        }
        else
        {
            IsFavourite = true;
        }
    }
    
    public async Task RemoveFavourite()
    {
        var result = await _restaurantService.RemoveFavourite(_restaurant.Id);
        if (result.IsFailure)
        {
            await _alertService.ShowAlertAsync("Błąd", result.Error);
            IsFavourite = true;
        }
        else
        {
            IsFavourite = false;
        }
    }
}