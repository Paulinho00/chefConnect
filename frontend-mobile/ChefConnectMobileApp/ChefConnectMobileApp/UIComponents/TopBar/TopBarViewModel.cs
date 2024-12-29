using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.TopBar;

public partial class TopBarViewModel : ObservableObject
{
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
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
}