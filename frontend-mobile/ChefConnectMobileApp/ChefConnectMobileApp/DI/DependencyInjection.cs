using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using ChefConnectMobileApp.UIComponents.LoginPage;
using ChefConnectMobileApp.UIComponents.MainPage;
using ChefConnectMobileApp.UIComponents.RegisterPage;

namespace ChefConnectMobileApp.DI;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IAuthService, AuthService>()
            .AddSingleton<INavigationService, NavigationService>()
            .AddTransient<IAlertService, AlertService>()
            .AddTransient<MainPage>()
            .AddTransient<RegisterPage>()
            .AddTransient<LoginPage>();

    }
}