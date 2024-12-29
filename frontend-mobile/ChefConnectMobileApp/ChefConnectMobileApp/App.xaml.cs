using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.Navigation;
using ChefConnectMobileApp.UIComponents.RegisterPage;

namespace ChefConnectMobileApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var navigationPage = new NavigationPage(new MainPage());
        navigationPage.BarBackgroundColor = Colors.Chocolate;
        var navigationService = ServiceHelper.GetService<INavigationService>();
        navigationService.SetNavigationPage(navigationPage);
        return new Window(navigationPage);
    }
}