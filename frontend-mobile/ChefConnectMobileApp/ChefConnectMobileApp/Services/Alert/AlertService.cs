namespace ChefConnectMobileApp.Services.Alert;

internal class AlertService : IAlertService
{
    public void ShowAlert(string title, string message, string cancel = "OK")
    {
        MainThread.BeginInvokeOnMainThread(() => Application.Current!.MainPage!.DisplayAlert(title, message, cancel));
    }
}