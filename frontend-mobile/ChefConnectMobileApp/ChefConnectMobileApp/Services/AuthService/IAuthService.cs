using ChefConnectMobileApp.Models;

namespace ChefConnectMobileApp.Services.AuthService;

public interface IAuthService
{
    User? GetCurrentUser();
}