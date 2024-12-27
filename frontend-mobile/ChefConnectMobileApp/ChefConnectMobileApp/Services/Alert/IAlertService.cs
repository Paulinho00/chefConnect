using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefConnectMobileApp.Services.Alert
{
    internal interface IAlertService
    {
        Task ShowAlertAsync(string title, string message, string cancel = "OK");
    }
}
