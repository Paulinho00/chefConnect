using ChefConnectMobileApp.Models;

namespace ChefConnectMobileApp.Services.AuthService;

public class AuthService : IAuthService
{
    private User? _currentUser;

    public User? GetCurrentUser()
        => _currentUser;
}