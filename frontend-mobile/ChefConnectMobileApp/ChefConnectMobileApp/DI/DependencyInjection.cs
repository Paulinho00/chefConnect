using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
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
            .AddTransient<MainPage>()
            .AddTransient<MainPageViewModel>()
            .AddTransient<RegisterPage>();
    }
}