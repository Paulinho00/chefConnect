using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services;

internal interface IRestaurantService
{
    Task<List<Restaurant>> GetAllRestaurants();
    Task<int> GetRatingOfRestaurant(int restaurantId);
    Task<bool> IsFavouriteForCurrentUser(int restaurantId);
    Task<Result<string>> AddNewFavourite(int restaurantId);
    Task<Result<string>> RemoveFavourite(int restaurantId);
}