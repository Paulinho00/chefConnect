namespace ChefConnectMobileApp.Services.Navigation;

internal class NavigationService : INavigationService
{
    private NavigationPage _navigationpage;
    public async Task TransitToPageAsync(Page page, bool clearStack = false)
    {
        if (clearStack)
        {
            _navigationpage = new NavigationPage(page);
            _navigationpage.BarBackgroundColor = Colors.Chocolate;
            App.Current.MainPage = _navigationpage;
        }
        else
        {
            await _navigationpage.Navigation.PushAsync(page);
        }
    }

    public void SetNavigationPage(NavigationPage page)
    {
        _navigationpage = page;
    }

    public async Task ReturnToPreviousPageAsync()
    {
        await _navigationpage.Navigation.PopAsync();
    }

    public void RemovePreviousPageFromStack()
    {
        var previousPage = _navigationpage.Navigation.NavigationStack[0];
        _navigationpage.Navigation.RemovePage(previousPage);
    }
}