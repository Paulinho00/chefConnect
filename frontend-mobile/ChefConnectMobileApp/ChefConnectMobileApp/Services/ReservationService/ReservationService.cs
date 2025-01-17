using System.Net.Http.Json;
using System.Text.Json;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Utils;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.ReservationService;

internal class ReservationService : IReservationService
{
    private HttpClient _httpClient;
    private const string _baseUrl = "/prod/reservations-service/";

    public ReservationService(IAuthService authService, HttpClient httpClient)
    {
        _httpClient = httpClient;
        httpClient.BaseAddress = new Uri(CloudConfig.ApiGatewayBaseUrl);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authService.GetAccessToken()}");
    }
    
    public async Task<List<(TimeSpan, int)>> GetTimeSlotsWithAvailableTables(Guid restaurantId, DateTime date)
    {
        var result =
            await _httpClient.GetAsync(
                _baseUrl + $"/available-tables?restaurantId={restaurantId.ToString()}&date={date:yyyy-MM-dd}");
        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var restaurants = JsonSerializer.Deserialize<List<(TimeSpan, int)>>(content);
            return restaurants ?? [];
        }
        return [];
    }

    public async Task<Result> MakeReservation(Guid restaurantId, DateTime date, int numberOfTables)
    {
        var requestBody = new MakeReservationRequestDto()
        {
            RestaurantId = restaurantId,
            DateTime = date,
            NumberOfSeats = numberOfTables
        };

        var result = await _httpClient.PostAsJsonAsync<MakeReservationRequestDto>(_baseUrl + "reservations/reserve", requestBody);
        return result.IsSuccessStatusCode ? Result.Success() : Result.Failure("Spróbuj ponownie za chwilę lub skontaktuj się z naszym działem wsparcia klienta");
    }

    public async Task<int> GetRatingOfRestaurant(Guid restaurantId)
    {
        var result = await _httpClient.GetAsync(_baseUrl + $"/opinions/average-rating/{restaurantId}");
        if(result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var averageOpinion = JsonSerializer.Deserialize<int>(content);
            return averageOpinion;
        }

        return -1;
    }

    public async Task<List<Reservation>> GetReservations()
    {
        var result =
            await _httpClient.GetAsync(
                _baseUrl + "/reservations");
        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var reservations = JsonSerializer.Deserialize<List<Reservation>>(content);
            return reservations ?? [];
        }
        return [];
    }

    public async Task<Result> CancelReservation(Guid reservationId)
    {
        var result = await _httpClient.PutAsync($"{_baseUrl}reservations/cancel/{reservationId.ToString()}", null);
        return result.IsSuccessStatusCode ? Result.Success() : Result.Failure("Spróbuj ponownie za chwilę lub skontaktuj się z naszym działem wsparcia klienta");
    }

    public async Task<Result> SendOpinion(ReservationOpinionDTO opinion)
    {
        var result = await _httpClient.PostAsJsonAsync(_baseUrl + "/opinions", opinion);
        return result.IsSuccessStatusCode ? Result.Success() : Result.Failure("Spróbuj ponownie za chwilę lub skontaktuj się z naszym działem wsparcia klienta");
    }
    
}