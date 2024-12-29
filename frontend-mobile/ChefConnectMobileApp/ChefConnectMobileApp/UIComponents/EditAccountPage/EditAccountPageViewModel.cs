using ChefConnectMobileApp.Services.Alert;
using ChefConnectMobileApp.Services.AuthService;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Models;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.EditAccountPage;

public partial class EditAccountPageViewModel : ObservableValidator
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

    public EditAccountPageViewModel()
    {
        var currentUser = _authService.GetCurrentUser();
        FirstName = currentUser.FirstName;
        LastName = currentUser.LastName;
        Email = currentUser.Email;
    }

    [RelayCommand]
    private async Task EditAccount()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            await _alertService.ShowAlertAsync("Błędne dane", GetErrors().FirstOrDefault().ErrorMessage);
            return;
        }

        var editDto = new EditAccountDto
        {
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
        };
        var result = await _authService.EditAccount(editDto);

        if (result.IsFailure)
        {
            await _alertService.ShowAlertAsync("Błąd", result.Error);
        }
        else
        {
            await _alertService.ShowAlertAsync("Pomyślnie zmieniono dane", "Nowe dane zostały poprawnie zapisane");
            await _navigationService.ReturnToPreviousPageAsync();
        }
    }
}