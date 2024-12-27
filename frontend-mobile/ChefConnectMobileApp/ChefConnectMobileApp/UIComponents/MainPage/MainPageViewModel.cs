using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content.Res;
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
    }
}
