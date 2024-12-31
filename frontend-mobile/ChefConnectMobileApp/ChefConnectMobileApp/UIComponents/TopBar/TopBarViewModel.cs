using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.TopBar;

public partial class TopBarViewModel : ObservableObject
{
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();
    private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();
    
    [ObservableProperty]
    private char _initialOfFirstNameOfCurrentUser;

    public TopBarViewModel()
    {
        var user = _authService.GetCurrentUser()!;
        _initialOfFirstNameOfCurrentUser = user.FirstName[0];
    }

    [RelayCommand]
    private async Task GoToReservationsListPage()
    {
        await _navigationService.TransitToPageAsync(new ReservationsListPage.ReservationsListPage(), false);
    }

    [RelayCommand]
    private async Task GoToAccountDetailsPage()
    {
        await _navigationService.TransitToPageAsync(new AccountDetailsPage.AccountDetailsPage(), false);
    }

    [RelayCommand]
    private async Task SignOut()
    {
        var result = await _authService.SignOutAsync();
        if (result.IsFailure)
        {
            await _alertService.ShowAlertAsync("Nie zostałeś wylogowany",
                "W wyniku błędu, nie zostałeś wylogowany. Spróbuj ponownie za chwilę");
        }
        else
        {
            await _alertService.ShowAlertAsync("Wylogowano", "Zostałeś wylogownay");
            await _navigationService.TransitToPageAsync(new ChefConnectMobileApp.MainPage(), true);
        }
    }
}