using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services;

internal interface IRestaurantService
{
    Task<Result<List<Restaurant>>> GetAllRestaurants();
    Task<Result<int>> GetRatingOfRestaurant(int restaurantId);
    Task<Result<bool>> IsFavouriteForCurrentUser(int restaurantId);
    Task<Result> AddNewFavourite(int restaurantId);
    Task<Result> RemoveFavourite(int restaurantId);
}