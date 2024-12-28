using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.AuthService;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IAuthService _authService;

    
    
    public RestaurantService(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<List<Restaurant>> GetAllRestaurants()
    {
        return _restaurants;
    }

    public async Task<int> GetRatingOfRestaurant(int restaurantId)
    {
        //TODO: Add call to API to get rating
        return 3;
    }

    public async Task<bool> IsFavouriteForCurrentUser(int restaurantId)
    {
        var user = _authService.GetCurrentUser();
        //TODO: Call to API
        return _favouriteRestaurants.Contains(restaurantId);
    }

    public async Task<Result<string>> AddNewFavourite(int restaurantId)
    {
        var user = _authService.GetCurrentUser();
        //TODO: call to API
        _favouriteRestaurants.Add(restaurantId);
        var result = new Result<string>();
        return result;
    }

    public async Task<Result<string>> RemoveFavourite(int restaurantId)
    {
        var user = _authService.GetCurrentUser();
        //TODO: call to API
        _favouriteRestaurants.Remove(restaurantId);
        var result = new Result<string>();
        return result;
    }
    
    private List<int> _favouriteRestaurants = new List<int>();
    private List<Restaurant> _restaurants = new List<Restaurant>()
    {
        new Restaurant
        {
            Address = "ul. xdxdxdxd",
            Id = 1,
            Name = "nazwa1",
            NumberOfTables = 25
        },
        new Restaurant
        {
            Address = "ul. qewqeqweqe",
            Id = 2,
            Name = "nazwa2",
            NumberOfTables = 20
        },
        new Restaurant
        {
            Address = "ul. ggggggg",
            Id = 3,
            Name = "nazwa3",
            NumberOfTables = 10
        }
    };
}