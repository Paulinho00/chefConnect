using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefConnectMobileApp.Services.Navigation
{
    internal class NavigationService : INavigationService
    {
        private NavigationPage _navigationpage;
        public async Task TransitToPage(Page page, bool clearStack = false)
        {
            if (clearStack)
            {
                _navigationpage = new NavigationPage(page);
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
    }
}
