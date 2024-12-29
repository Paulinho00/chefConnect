
namespace ChefConnectMobileApp.Services.Alert;

internal interface IAlertService
{
    Task ShowAlertAsync(string title, string message, string cancel = "OK");
}