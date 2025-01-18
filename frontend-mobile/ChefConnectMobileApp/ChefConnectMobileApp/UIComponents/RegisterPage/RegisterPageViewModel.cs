using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace ChefConnectMobileApp.UIComponents.RegisterPage;

public partial class RegisterPageViewModel : ObservableValidator
{
    private IAuthService _authService = ServiceHelper.GetService<IAuthService>();
    private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();
    private IAlertService _alertService = ServiceHelper.GetService<IAlertService>();

    [ObservableProperty]
    [Required]
    private string _firstName;

    [ObservableProperty]
    [Required]
    private string _lastName;

    [ObservableProperty]
    [Required]
    [MinLength(5)]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
    private string _email;

    [ObservableProperty]
    [Required]
    [MinLength(8)]
    private string _password;

    [RelayCommand]
    private async Task SignUpAsync()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            var error = GetErrors().FirstOrDefault();
            _alertService.ShowAlert("Błędne dane", error.ErrorMessage);
            return;
        }
        var signUpRequest = new SignUpData()
        {
            Email = _email,
            FirstName = _firstName,
            LastName = _lastName,
            Password = _password
        };

        var result = await _authService.SignUpAsync(signUpRequest);
        if (result.IsFailure)
        {
            _alertService.ShowAlert("Błąd", result.Error);
        }
        else
        {
            await _navigationService.TransitToPageAsync(new ConfirmAccountPage.ConfirmAccountPage(), false);
            _navigationService.RemovePreviousPageFromStack();
        }
    }

    [RelayCommand]
    private async Task GoToConfirmationPageAsync()
    {
        await _navigationService.TransitToPageAsync(new ConfirmAccountPage.ConfirmAccountPage(), false);
    }
}