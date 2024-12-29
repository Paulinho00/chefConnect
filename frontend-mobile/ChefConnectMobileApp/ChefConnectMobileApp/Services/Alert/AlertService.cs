using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefConnectMobileApp.Services.Alert;

internal class AlertService : IAlertService
{
    public Task ShowAlertAsync(string title, string message, string cancel = "OK")
    {
        return Application.Current!.MainPage!.DisplayAlert(title, message, cancel);
    }
}