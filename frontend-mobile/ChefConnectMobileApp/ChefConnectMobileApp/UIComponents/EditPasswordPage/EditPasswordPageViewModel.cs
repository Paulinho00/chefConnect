using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.EditPasswordPage;

public partial class EditPasswordPageViewModel : ObservableValidator
{
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
    private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();

    [ObservableProperty]
    [Required]
    [MinLength(8)]
    private string _newPassword;

    [ObservableProperty]
    [Required]
    [MinLength(8)]
    private string _newPasswordRepeated;

    [ObservableProperty]
    private bool _arePasswordsEqual;


    partial void OnNewPasswordChanged(string value)
    {
        ArePasswordsEqual = NewPassword == NewPasswordRepeated;
    }

    partial void OnNewPasswordRepeatedChanged(string value)
    {
        ArePasswordsEqual = NewPassword == NewPasswordRepeated;
    }

    [RelayCommand]
    private async Task ChangePassword()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await _alertService.ShowAlertAsync("Błędne hasło", "Hasło jest za krótkie");
            return;
        }

        var result = await _authService.EditPasswordAsync(NewPassword);
        if (result.IsFailure)
        {
            await _alertService.ShowAlertAsync("Błąd", result.Error);
        }
        else
        {
            await _alertService.ShowAlertAsync("Pomyślnie zmieniono hasło", "Hasło zostało pomyślnie ustawione");
            await _navigationService.ReturnToPreviousPageAsync();
        }
    }
}