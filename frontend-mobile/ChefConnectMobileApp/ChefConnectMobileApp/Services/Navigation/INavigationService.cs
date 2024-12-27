using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChefConnectMobileApp.UIComponents.RegisterPage;

namespace ChefConnectMobileApp.Services.Navigation
{
    public interface INavigationService
    {
        Task TransitToPage(Page page, bool clearStack = false);
        void SetNavigationPage(NavigationPage page);
    }
}
