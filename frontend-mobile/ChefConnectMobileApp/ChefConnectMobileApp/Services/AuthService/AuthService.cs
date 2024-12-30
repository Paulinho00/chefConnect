using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Utils;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Services.AuthService;

internal class AuthService : IAuthService
{
    private User? _currentUser;
    private readonly AmazonCognitoIdentityProviderClient _cognitoClient;
    private readonly string _clientId;
    private readonly string _userPoolId;

    public User? GetCurrentUser()
        => _currentUser;

    public AuthService()
    {
        var credentials = new Amazon.Runtime.BasicAWSCredentials(CloudConfig.AccessKeyId, CloudConfig.SecretAccessKey);
        _cognitoClient = new AmazonCognitoIdentityProviderClient(credentials, RegionEndpoint.GetBySystemName("us-east-1"));
        _clientId = CloudConfig.CognitoClientId;
    }

    public async Task<Result> SignUpAsync(SignUpData data)
    {
        try
        {
            var attributes = new List<AttributeType>
            {
                new AttributeType { Name = "email", Value = data.Email },
                new AttributeType { Name = "given_name", Value = data.FirstName },
                new AttributeType { Name = "family_name", Value = data.LastName }
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
        //TODO: Add  call to API/Cognito
        var result = new Result();

        _currentUser = new User
        {
            Email = "test@gmail.com",
            FirstName = "test",
            LastName = "Test",
        };

        return result;
    }

    public async Task<Result> SignOutAsync()
    {
        //TODO: Call to Cognito
        return new Result<string>();
    }

    public async Task<Result> EditAccountAsync(EditAccountDto data)
    {
        //TODO: Call to Cognito
        return new Result<string>();

    }

    public async Task<Result> EditPasswordAsync(string password)
    {
        //TODO: Call to Cognito
        return new Result<string>();
    }
}