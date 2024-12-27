using ChefConnectMobileApp.DI;
using ChefConnectMobileApp.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChefConnectMobileApp.UIComponents.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        private INavigationService _navigationService = ServiceHelper.GetService<INavigationService>();

        [RelayCommand]
        private async Task GoToRegistrationPage()
        {
            await _navigationService.TransitToPageAsync(new RegisterPage.RegisterPage());
        }

        [RelayCommand]
        private async Task GoToLoginPage()
        {
            await _navigationService.TransitToPageAsync(new LoginPage.LoginPage());
        }
    }
}
