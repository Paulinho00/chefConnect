using System.ComponentModel.DataAnnotations;
using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.LoginPage;

public partial class LoginPageViewModel : ObservableValidator
{
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
    private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();
    
    [ObservableProperty]
    [Required]
    private string _email;

    [ObservableProperty]
    [Required]
    private string _password;

    [RelayCommand]
    private async Task SignIn()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            _alertService.ShowAlert("Błąd", "Pola nie mogą być puste");
            return;
        }

        var result = await _authService.SignInAsync(_email, _password);
        if (result.IsFailure)
        {
           _alertService.ShowAlert("Błąd", result.Error);
        }
        else
        {
            _navigationService.TransitToPageAsync(new RestaurantsListPage.RestaurantsListPage(), true);
        }

    }
}