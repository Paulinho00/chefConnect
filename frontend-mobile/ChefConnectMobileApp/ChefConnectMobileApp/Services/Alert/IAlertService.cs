
namespace ChefConnectMobileApp.Services.Alert;

internal interface IAlertService
{
    void ShowAlert(string title, string message, string cancel = "OK");
}