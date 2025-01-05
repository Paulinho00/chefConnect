﻿using System.Text.Json;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.AuthService;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services;

internal class RestaurantService : IRestaurantService
{
    private readonly HttpClient _httpClient;
    private const string _baseUrl = "https://xydzsl34r0.execute-api.us-east-1.amazonaws.com";

    public RestaurantService(IAuthService authService, HttpClient httpClient)
    {
        _httpClient = httpClient;
        httpClient.BaseAddress = new Uri(_baseUrl);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authService.GetAccessToken()}");
    }
    
    public async Task<List<Restaurant>> GetAllRestaurants()
    {
        
        var response = await _httpClient.GetAsync("/prod/restaurants-service/restaurants/all").ConfigureAwait(false);
        if(response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(content);
            return restaurants ?? new List<Restaurant>();
        }

        return new List<Restaurant>();
    }

    public async Task<bool> IsFavouriteForCurrentUser(Guid restaurantId)
    {
        var response = await _httpClient.GetAsync($"/prod/restaurants-service/user-preferences").ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var favouriteRestaurants = JsonSerializer.Deserialize<List<Guid>>(content);
            return favouriteRestaurants != null && favouriteRestaurants.Contains(restaurantId);
        }

        return false;
    }

    public async Task<Result> AddNewFavourite(Guid restaurantId)
    {
        var response = await _httpClient.PostAsync($"/prod/restaurants-service/user-preferences/add/{restaurantId}", null).ConfigureAwait(false);
        if(response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return Result.Failure("Spróbuj jeszcze raz");
    }

    public async Task<Result> RemoveFavourite(Guid restaurantId)
    {
        var response = await _httpClient.PostAsync($"/prod/restaurants-service/user-preferences/remove/{restaurantId}", null).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return Result.Failure("Spróbuj jeszcze raz");
    }
}