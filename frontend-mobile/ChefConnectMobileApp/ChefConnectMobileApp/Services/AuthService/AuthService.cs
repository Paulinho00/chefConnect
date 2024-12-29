using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.AuthService;

internal class AuthService : IAuthService
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

    public async Task<Result<string>> SignInAsync(string email, string password)
    {
        //TODO: Add  call to API/Cognito
        var result = new Result<string>();

        _currentUser = new User
        {
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "Test",
            Password = "test123"
        };

        return result;
    }

    public async Task<Result<string>> SignOutAsync()
    {
        //TODO: Call to Cognito
        return new Result<string>();
    }

    public async Task<Result<string>> EditAccount(EditAccountDto data)
    {
        //TODO: Call to Cognito
        return new Result<string>();

    }
}