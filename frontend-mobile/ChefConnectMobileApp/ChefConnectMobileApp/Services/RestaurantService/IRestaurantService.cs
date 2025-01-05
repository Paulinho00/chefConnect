using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services;

internal interface IRestaurantService
{
    Task<List<Restaurant>> GetAllRestaurants();
    Task<bool> IsFavouriteForCurrentUser(Guid restaurantId);
    Task<Result> AddNewFavourite(Guid restaurantId);
    Task<Result> RemoveFavourite(Guid restaurantId);
}