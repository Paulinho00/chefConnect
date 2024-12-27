using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.AuthService;

public interface IAuthService
{
    User? GetCurrentUser();
    Task<Result<string>> SignUpAsync(SignUpData data);
    Task<Result<string>> SignInAsync(string email, string password);
}