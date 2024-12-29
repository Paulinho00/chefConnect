using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.AuthService;

internal interface IAuthService
{
    User? GetCurrentUser();
    Task<Result<string>> SignUpAsync(SignUpData data);
    Task<Result<string>> SignInAsync(string email, string password);
    Task<Result<string>> SignOutAsync();
    Task<Result<string>> EditAccountAsync(EditAccountDto  data);
    Task<Result<string>> EditPasswordAsync(string password);
}