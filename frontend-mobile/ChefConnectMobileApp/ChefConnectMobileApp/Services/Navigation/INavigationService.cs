namespace ChefConnectMobileApp.Services.Navigation;

public interface INavigationService
{
    Task TransitToPageAsync(Page page, bool clearStack = false);
    void SetNavigationPage(NavigationPage page);
    Task ReturnToPreviousPageAsync();
}