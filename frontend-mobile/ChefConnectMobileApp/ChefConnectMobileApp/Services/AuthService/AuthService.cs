using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Utils;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.AuthService;

internal class AuthService : IAuthService
{
    private User? _currentUser;
    private readonly IAmazonCognitoIdentityProvider _cognitoClient;
    private readonly string _clientId;
    private string _accessToken;

    public User? GetCurrentUser()
        => _currentUser;

    public AuthService(IAmazonCognitoIdentityProvider cognitoClient)
    {
        _cognitoClient = cognitoClient;
        _clientId = CloudConfig.CognitoClientId;
    }

    public async Task<Result> SignUpAsync(SignUpData data)
    {
        try
        {
            var attributes = new List<AttributeType>
            {
                new() { Name = "email", Value = data.Email },
                new() { Name = "given_name", Value = data.FirstName },
                new() { Name = "family_name", Value = data.LastName }
            };

            var request = new SignUpRequest
            {
                ClientId = _clientId,
                Username = data.Email,
                Password = data.Password,
                UserAttributes = attributes
            };

            var response = await _cognitoClient.SignUpAsync(request);
            return Result.Success();
        }
        catch (UsernameExistsException)
        {
            return Result.Failure("Ten email jest już zajęty");
        }
        catch (InvalidPasswordException)
        {
            return Result.Failure("Nieprawidłowe hasło");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Rejestracja nie powiodła się: {ex.Message}");
        }
    }

    public async Task<Result> SignInAsync(string email, string password)
    {
        try
        {
            var authParameters = new Dictionary<string, string>
            {
                { "USERNAME", email },
                { "PASSWORD", password }
            };

            var request = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _clientId,
                AuthParameters = authParameters
            };

            var response = await _cognitoClient.InitiateAuthAsync(request);
            _accessToken = response.AuthenticationResult.AccessToken;
            _currentUser = await GetCurrentUserAsync();
            return Result.Success();
        }
        catch (UserNotConfirmedException)
        {
            return Result.Failure("Musisz potwierdzić swoje konto");
        }
        catch (NotAuthorizedException)
        {
            return Result.Failure("Nieprawidłowe hasło lub email");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Błąd: {ex.Message}");
        }
    }

    public async Task<Result> SignOutAsync()
    {
        _accessToken = string.Empty;
        _currentUser = null;
        return new Result<string>();
    }

    public async Task<Result> EditAccountAsync(EditAccountDto data)
    {
        if (string.IsNullOrEmpty(_accessToken))
        {
            return Result.Failure("Nie jesteś zalogowany");
        }

        try
        {
            var attributes = new List<AttributeType>
            {
                new AttributeType { Name = "given_name", Value = data.FirstName },
                new AttributeType { Name = "family_name", Value = data.LastName },
                new AttributeType { Name = "email", Value = data.Email }
            };

            var request = new UpdateUserAttributesRequest
            {
                AccessToken = _accessToken,
                UserAttributes = attributes
            };

            await _cognitoClient.UpdateUserAttributesAsync(request);
            _currentUser = await GetCurrentUserAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure($"Błąd zmiany danych: {e.Message}");
        }

    }

    public async Task<Result> EditPasswordAsync(string oldPassword, string newPassword)
    {
        if (string.IsNullOrEmpty(_accessToken))
        {
            return Result.Failure("Nie jesteś zalogowany");
        }

        try
        {
            var request = new ChangePasswordRequest
            {
                AccessToken = _accessToken,
                PreviousPassword = oldPassword,
                ProposedPassword = newPassword
            };

            await _cognitoClient.ChangePasswordAsync(request);
            return Result.Success();
        }
        catch (InvalidPasswordException)
        {
            return Result.Failure("Nowe hasło nie spełnia wymagań");
        }
        catch (NotAuthorizedException)
        {
            return Result.Failure("Obecne hasło jest nieprawidłowe");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Błąd zmiany hasła: {ex.Message}"); ;
        }
    }

    public async Task<Result> ConfirmAccountAsync(string email, string confirmationCode)
    {
        try
        {
            var request = new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = email,
                ConfirmationCode = confirmationCode
            };

            await _cognitoClient.ConfirmSignUpAsync(request);
            return Result.Success();
        }
        catch (CodeMismatchException)
        {
            return Result.Failure("Niepoprawny kod");
        }
        catch (ExpiredCodeException)
        {
            return Result.Failure("Kod wygasł");
        }
        catch (UserNotFoundException)
        {
            return Result.Failure("Niepoprawny email");
        }
        catch (Exception e)
        {
            return Result.Failure($"Błąd przy potwierdzaniu konta: {e.Message}");
        }
    }

    public async Task<Result> ResendConfirmationCodeAsync(string email)
    {
        try
        {
            var request = new ResendConfirmationCodeRequest
            {
                ClientId = _clientId,
                Username = email
            };
            var result = await _cognitoClient.ResendConfirmationCodeAsync(request);
            return Result.Success();
        }
        catch (CodeDeliveryFailureException)
        {
            return Result.Failure("Błąd dostarczenia kodu, spróbuj ponownie później");
        }
        catch (UserNotFoundException)
        {
            return Result.Failure("Niepoprawny email");
        }
        catch (Exception e)
        {
            return Result.Failure($"Błąd wysyłania kodu: {e.Message}");
        }
    }


    private async Task<User> GetCurrentUserAsync()
    {
        var request = new GetUserRequest
        {
            AccessToken = _accessToken
        };

        var response = await _cognitoClient.GetUserAsync(request);

        return new User
        {
            Id = response.Username,
            Email = response.UserAttributes.FirstOrDefault(a => a.Name == "email")?.Value,
            FirstName = response.UserAttributes.FirstOrDefault(a => a.Name == "given_name")?.Value,
            LastName = response.UserAttributes.FirstOrDefault(a => a.Name == "family_name")?.Value
        };
    }
}