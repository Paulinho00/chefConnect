using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services.AuthService;
using Moq;

namespace ChefConnectMobileApp.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        private Mock<IAmazonCognitoIdentityProvider> _mockCognitoClient;
        private AuthService _authService;

        [TestInitialize]
        public void Setup()
        {
            _mockCognitoClient = new Mock<IAmazonCognitoIdentityProvider>();
            _authService = new AuthService(_mockCognitoClient.Object);
        }

        [TestMethod]
        public async Task SignUpAsync_ShouldReturnSuccess_WhenSignUpIsSuccessful()
        {
            // Arrange
            var signUpData = new SignUpData
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!"
            };
            _mockCognitoClient.Setup(x => x.SignUpAsync(It.IsAny<SignUpRequest>(), default))
                .ReturnsAsync(new SignUpResponse());

            // Act
            var result = await _authService.SignUpAsync(signUpData);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task SignInAsync_ShouldReturnSuccess_WhenSignInIsSuccessful()
        {
            // Arrange
            var email = "john.doe@example.com";
            var password = "Password123!";
            _mockCognitoClient.Setup(x => x.InitiateAuthAsync(It.IsAny<InitiateAuthRequest>(), default))
                .ReturnsAsync(new InitiateAuthResponse
                {
                    AuthenticationResult = new AuthenticationResultType
                    {
                        AccessToken = "access_token"
                    }
                });
            _mockCognitoClient.Setup(x => x.GetUserAsync(It.IsAny<GetUserRequest>(), default))
                .ReturnsAsync(new GetUserResponse
                {
                    Username = "user_id",
                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "email", Value = email },
                        new AttributeType { Name = "given_name", Value = "John" },
                        new AttributeType { Name = "family_name", Value = "Doe" }
                    }
                });

            // Act
            var result = await _authService.SignInAsync(email, password);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task SignOutAsync_ShouldReturnSuccess()
        {
            // Act
            var result = await _authService.SignOutAsync();

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task EditAccountAsync_ShouldReturnSuccess_WhenEditIsSuccessful()
        {
            // Arrange
            var editAccountData = new EditAccountDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            await SetupSignInUser();
            _mockCognitoClient.Setup(x => x.UpdateUserAttributesAsync(It.IsAny<UpdateUserAttributesRequest>(), default))
                .ReturnsAsync(new UpdateUserAttributesResponse());
            _mockCognitoClient.Setup(x => x.GetUserAsync(It.IsAny<GetUserRequest>(), default))
                .ReturnsAsync(new GetUserResponse
                {
                    Username = "user_id",
                    UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "email", Value = "john.doe@example.com" },
                        new AttributeType { Name = "given_name", Value = "John" },
                        new AttributeType { Name = "family_name", Value = "Doe" }
                    }
                });

            // Act
            var result = await _authService.EditAccountAsync(editAccountData);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task EditPasswordAsync_ShouldReturnSuccess_WhenEditIsSuccessful()
        {
            // Arrange
            var oldPassword = "OldPassword123!";
            var newPassword = "NewPassword123!";
            await SetupSignInUser();
            _mockCognitoClient.Setup(x => x.ChangePasswordAsync(It.IsAny<ChangePasswordRequest>(), default))
                .ReturnsAsync(new ChangePasswordResponse());

            // Act
            var result = await _authService.EditPasswordAsync(oldPassword, newPassword);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task ConfirmAccountAsync_ShouldReturnSuccess_WhenConfirmationIsSuccessful()
        {
            // Arrange
            var email = "john.doe@example.com";
            var confirmationCode = "123456";
            _mockCognitoClient.Setup(x => x.ConfirmSignUpAsync(It.IsAny<ConfirmSignUpRequest>(), default))
                .ReturnsAsync(new ConfirmSignUpResponse());

            // Act
            var result = await _authService.ConfirmAccountAsync(email, confirmationCode);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task ResendConfirmationCodeAsync_ShouldReturnSuccess_WhenResendIsSuccessful()
        {
            // Arrange
            var email = "john.doe@example.com";
            _mockCognitoClient.Setup(x => x.ResendConfirmationCodeAsync(It.IsAny<ResendConfirmationCodeRequest>(), default))
                .ReturnsAsync(new ResendConfirmationCodeResponse());

            // Act
            var result = await _authService.ResendConfirmationCodeAsync(email);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task SignUpAsync_ShouldReturnFailure_WhenUsernameExistsExceptionThrown()
        {
            // Arrange
            var signUpData = new SignUpData
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!"
            };
            _mockCognitoClient.Setup(x => x.SignUpAsync(It.IsAny<SignUpRequest>(), default))
                .ThrowsAsync(new UsernameExistsException("Username exists"));

            // Act
            var result = await _authService.SignUpAsync(signUpData);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Ten email jest już zajęty", result.Error);
        }

        [TestMethod]
        public async Task SignUpAsync_ShouldReturnFailure_WhenInvalidPasswordExceptionThrown()
        {
            // Arrange
            var signUpData = new SignUpData
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!"
            };
            _mockCognitoClient.Setup(x => x.SignUpAsync(It.IsAny<SignUpRequest>(), default))
                .ThrowsAsync(new InvalidPasswordException("Invalid password"));

            // Act
            var result = await _authService.SignUpAsync(signUpData);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Nieprawidłowe hasło", result.Error);
        }

        [TestMethod]
        public async Task SignUpAsync_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var signUpData = new SignUpData
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!"
            };
            _mockCognitoClient.Setup(x => x.SignUpAsync(It.IsAny<SignUpRequest>(), default))
                .ThrowsAsync(new System.Exception("Unknown error"));

            // Act
            var result = await _authService.SignUpAsync(signUpData);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Rejestracja nie powiodła się: Unknown error", result.Error);
        }

        [TestMethod]
        public async Task SignInAsync_ShouldReturnFailure_WhenUserNotConfirmedExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            var password = "Password123!";
            _mockCognitoClient.Setup(x => x.InitiateAuthAsync(It.IsAny<InitiateAuthRequest>(), default))
                .ThrowsAsync(new UserNotConfirmedException("User not confirmed"));

            // Act
            var result = await _authService.SignInAsync(email, password);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Musisz potwierdzić swoje konto", result.Error);
        }

        [TestMethod]
        public async Task SignInAsync_ShouldReturnFailure_WhenNotAuthorizedExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            var password = "Password123!";
            _mockCognitoClient.Setup(x => x.InitiateAuthAsync(It.IsAny<InitiateAuthRequest>(), default))
                .ThrowsAsync(new NotAuthorizedException("Not authorized"));

            // Act
            var result = await _authService.SignInAsync(email, password);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Nieprawidłowe hasło lub email", result.Error);
        }

        [TestMethod]
        public async Task SignInAsync_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            var password = "Password123!";
            _mockCognitoClient.Setup(x => x.InitiateAuthAsync(It.IsAny<InitiateAuthRequest>(), default))
                .ThrowsAsync(new System.Exception("Unknown error"));

            // Act
            var result = await _authService.SignInAsync(email, password);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Błąd: Unknown error", result.Error);
        }

        [TestMethod]
        public async Task EditAccountAsync_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var editAccountData = new EditAccountDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            await SetupSignInUser();

            _mockCognitoClient.Setup(x => x.UpdateUserAttributesAsync(It.IsAny<UpdateUserAttributesRequest>(), default))
                .ThrowsAsync(new System.Exception("Unknown error"));

            // Act
            var result = await _authService.EditAccountAsync(editAccountData);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Błąd zmiany danych: Unknown error", result.Error);
        }

        [TestMethod]
        public async Task EditPasswordAsync_ShouldReturnFailure_WhenInvalidPasswordExceptionThrown()
        {
            // Arrange
            var oldPassword = "OldPassword123!";
            var newPassword = "NewPassword123!";
            await SetupSignInUser();
            _mockCognitoClient.Setup(x => x.ChangePasswordAsync(It.IsAny<ChangePasswordRequest>(), default))
                .ThrowsAsync(new InvalidPasswordException("Invalid password"));

            // Act
            var result = await _authService.EditPasswordAsync(oldPassword, newPassword);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Nowe hasło nie spełnia wymagań", result.Error);
        }

        [TestMethod]
        public async Task EditPasswordAsync_ShouldReturnFailure_WhenNotAuthorizedExceptionThrown()
        {
            // Arrange
            var oldPassword = "OldPassword123!";
            var newPassword = "NewPassword123!";
            await SetupSignInUser();
            _mockCognitoClient.Setup(x => x.ChangePasswordAsync(It.IsAny<ChangePasswordRequest>(), default))
                .ThrowsAsync(new NotAuthorizedException("Not authorized"));

            // Act
            var result = await _authService.EditPasswordAsync(oldPassword, newPassword);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Obecne hasło jest nieprawidłowe", result.Error);
        }

        [TestMethod]
        public async Task EditPasswordAsync_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var oldPassword = "OldPassword123!";
            var newPassword = "NewPassword123!";
            await SetupSignInUser();
            _mockCognitoClient.Setup(x => x.ChangePasswordAsync(It.IsAny<ChangePasswordRequest>(), default))
                .ThrowsAsync(new System.Exception("Unknown error"));

            // Act
            var result = await _authService.EditPasswordAsync(oldPassword, newPassword);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Błąd zmiany hasła: Unknown error", result.Error);
        }

        [TestMethod]
        public async Task ConfirmAccountAsync_ShouldReturnFailure_WhenCodeMismatchExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            var confirmationCode = "123456";
            _mockCognitoClient.Setup(x => x.ConfirmSignUpAsync(It.IsAny<ConfirmSignUpRequest>(), default))
                .ThrowsAsync(new CodeMismatchException("Code mismatch"));

            // Act
            var result = await _authService.ConfirmAccountAsync(email, confirmationCode);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Niepoprawny kod", result.Error);
        }

        [TestMethod]
        public async Task ConfirmAccountAsync_ShouldReturnFailure_WhenExpiredCodeExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            var confirmationCode = "123456";
            _mockCognitoClient.Setup(x => x.ConfirmSignUpAsync(It.IsAny<ConfirmSignUpRequest>(), default))
                .ThrowsAsync(new ExpiredCodeException("Expired code"));

            // Act
            var result = await _authService.ConfirmAccountAsync(email, confirmationCode);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Kod wygasł", result.Error);
        }

        [TestMethod]
        public async Task ConfirmAccountAsync_ShouldReturnFailure_WhenUserNotFoundExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            var confirmationCode = "123456";
            _mockCognitoClient.Setup(x => x.ConfirmSignUpAsync(It.IsAny<ConfirmSignUpRequest>(), default))
                .ThrowsAsync(new UserNotFoundException("User not found"));

            // Act
            var result = await _authService.ConfirmAccountAsync(email, confirmationCode);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Niepoprawny email", result.Error);
        }

        [TestMethod]
        public async Task ConfirmAccountAsync_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            var confirmationCode = "123456";
            _mockCognitoClient.Setup(x => x.ConfirmSignUpAsync(It.IsAny<ConfirmSignUpRequest>(), default))
                .ThrowsAsync(new System.Exception("Unknown error"));

            // Act
            var result = await _authService.ConfirmAccountAsync(email, confirmationCode);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Błąd przy potwierdzaniu konta: Unknown error", result.Error);
        }

        [TestMethod]
        public async Task ResendConfirmationCodeAsync_ShouldReturnFailure_WhenCodeDeliveryFailureExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            _mockCognitoClient.Setup(x => x.ResendConfirmationCodeAsync(It.IsAny<ResendConfirmationCodeRequest>(), default))
                .ThrowsAsync(new CodeDeliveryFailureException("Code delivery failure"));

            // Act
            var result = await _authService.ResendConfirmationCodeAsync(email);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Błąd dostarczenia kodu, spróbuj ponownie później", result.Error);
        }

        [TestMethod]
        public async Task ResendConfirmationCodeAsync_ShouldReturnFailure_WhenUserNotFoundExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            _mockCognitoClient.Setup(x => x.ResendConfirmationCodeAsync(It.IsAny<ResendConfirmationCodeRequest>(), default))
                .ThrowsAsync(new UserNotFoundException("User not found"));

            // Act
            var result = await _authService.ResendConfirmationCodeAsync(email);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Niepoprawny email", result.Error);
        }

        [TestMethod]
        public async Task ResendConfirmationCodeAsync_ShouldReturnFailure_WhenExceptionThrown()
        {
            // Arrange
            var email = "john.doe@example.com";
            _mockCognitoClient.Setup(x => x.ResendConfirmationCodeAsync(It.IsAny<ResendConfirmationCodeRequest>(), default))
                .ThrowsAsync(new System.Exception("Unknown error"));

            // Act
            var result = await _authService.ResendConfirmationCodeAsync(email);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Błąd wysyłania kodu: Unknown error", result.Error);
        }

        private async Task SetupSignInUser()
        {
            var response = new InitiateAuthResponse
            {
                AuthenticationResult = new AuthenticationResultType
                {
                    AccessToken = "access_token"
                }
            };
            _mockCognitoClient.Setup(x => x.InitiateAuthAsync(It.IsAny<InitiateAuthRequest>(), default)).ReturnsAsync(response);
            await _authService.SignInAsync("test", "test");
        }
    }
}
