using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.AuthService;

public class AuthService : IAuthService
{
    private User? _currentUser;

    public User? GetCurrentUser()
        => _currentUser;

    public async Task<Result<string>> SignUpAsync(SignUpData data)
    {
        //TODO: Add call to API/Cognito
        var result = new Result<string>();

        return result;
    }
}