using Amazon;
using Amazon.CognitoIdentityProvider;
using ChefConnectMobileApp.Services;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using ChefConnectMobileApp.Services.ReservationService;
using ChefConnectMobileApp.Utils;

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
            .AddTransient<IReservationService, ReservationService>()
            .AddSingleton<IAmazonCognitoIdentityProvider>(new AmazonCognitoIdentityProviderClient(
                new Amazon.Runtime.BasicAWSCredentials(
                    CloudConfig.AccessKeyId,
                    CloudConfig.SecretAccessKey),
                RegionEndpoint.GetBySystemName("us-east-1")))
            .AddTransient<HttpClient>();


    }
}