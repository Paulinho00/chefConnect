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
    [Required]
    [MinLength(8)]
    private string _oldPassword;

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
            _alertService.ShowAlert("Błędne hasło", "Hasło jest za krótkie");
            return;
        }

        var result = await _authService.EditPasswordAsync(OldPassword, NewPassword);
        if (result.IsFailure)
        {
            _alertService.ShowAlert("Błąd", result.Error);
        }
        else
        {
            _alertService.ShowAlert("Pomyślnie zmieniono hasło", "Hasło zostało pomyślnie ustawione");
            _navigationService.ReturnToPreviousPageAsync();
        }
    }
}