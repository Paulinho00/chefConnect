using ChefConnectMobileApp.Models;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.AuthService;

internal interface IAuthService
{
    User? GetCurrentUser();
    Task<Result> SignUpAsync(SignUpData data);
    Task<Result> SignInAsync(string email, string password);
    Task<Result> SignOutAsync();
    Task<Result> EditAccountAsync(EditAccountDto data);
    Task<Result> EditPasswordAsync(string oldPassword, string newPassword);
    Task<Result> ConfirmAccountAsync(string email, string confirmationCode);
    Task<Result> ResendConfirmationCodeAsync(string email);
}