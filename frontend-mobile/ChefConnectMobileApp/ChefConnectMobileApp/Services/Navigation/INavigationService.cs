namespace ChefConnectMobileApp.Services.Navigation;

internal interface INavigationService
{
    Task TransitToPageAsync(Page page, bool clearStack = false);
    void SetNavigationPage(NavigationPage page);
    Task ReturnToPreviousPageAsync();
}