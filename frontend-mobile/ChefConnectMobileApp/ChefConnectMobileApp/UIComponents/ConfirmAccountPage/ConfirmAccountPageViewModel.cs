using System.ComponentModel.DataAnnotations;
using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.ConfirmAccountPage;

public partial class ConfirmAccountPageViewModel : ObservableValidator
{
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
    private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();

    [ObservableProperty]
    [Required]
    [MinLength(6)]
    private string _confirmationCode;

    [ObservableProperty]
    [Required]
    [MinLength(5)]
    private string _email;

    [RelayCommand]
    private async Task ConfirmAccountAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await _alertService.ShowAlertAsync("Niepoprawne dane", "Kod lub email jest niepoprawny");
            return;
        }

        var result = await _authService.ConfirmAccountAsync(Email, ConfirmationCode);
        if (result.IsFailure)
            await _alertService.ShowAlertAsync("Błąd", result.Error);
        else
        {
            await _alertService.ShowAlertAsync("Konto potwierdzone", "");
            await _navigationService.TransitToPageAsync(new ChefConnectMobileApp.MainPage(), true);
        }
    }

    [RelayCommand]
    private async Task ResendConfirmationCodeAsync()
    {
        if (!string.IsNullOrWhiteSpace(_email))
        {
            await _alertService.ShowAlertAsync("Niepoprawny email", "");
        }

        var result = await _authService.ResendConfirmationCodeAsync(Email);
        if (result.IsFailure)
            await _alertService.ShowAlertAsync("Błąd", result.Error);
        else
        {
            await _alertService.ShowAlertAsync("Kod został wysłany", "");
        }

    }
}