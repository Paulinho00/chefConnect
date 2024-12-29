using ChefConnectMobileApp.Services;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using ChefConnectMobileApp.Services.ReservationService;

namespace ChefConnectMobileApp.DI;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IAuthService, AuthService>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddTransient<IAlertService, AlertService>()
            .AddTransient<IRestaurantService, RestaurantService>()
            .AddTransient<IReservationService, ReservationService>();

    }
}