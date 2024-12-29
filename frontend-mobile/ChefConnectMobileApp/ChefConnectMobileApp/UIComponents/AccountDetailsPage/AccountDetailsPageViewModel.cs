using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.AccountDetailsPage;

public partial class AccountDetailsPageViewModel : ObservableObject
{
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
    private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();

    [ObservableProperty]
    private User _currentUser;

    public AccountDetailsPageViewModel()
    {
        CurrentUser = _authService.GetCurrentUser()!;
    }

    [RelayCommand]
    private async Task GoToEditPage()
    {
        await _navigationService.TransitToPageAsync(new EditAccountPage.EditAccountPage(), false);
    }

}