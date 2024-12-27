using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.AuthService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChefConnectMobileApp.UIComponents.TopBar;

public partial class TopBarViewModel : ObservableObject
{
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
    
    [ObservableProperty]
    private char _initialOfFirstNameOfCurrentUser;

    public TopBarViewModel()
    {
        var user = _authService.GetCurrentUser()!;
        _initialOfFirstNameOfCurrentUser = user.FirstName[0];
    }
}