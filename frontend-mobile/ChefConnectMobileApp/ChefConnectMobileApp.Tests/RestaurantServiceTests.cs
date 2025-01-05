using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ChefConnectMobileApp.Models;
using ChefConnectMobileApp.Services;
using ChefConnectMobileApp.Services.AuthService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using CSharpFunctionalExtensions;

namespace ChefConnectMobileApp.Tests.Services;

[TestClass]
public class RestaurantServiceTests
{
    private Mock<IAuthService> _authServiceMock;
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private HttpClient _httpClient;
    private RestaurantService _restaurantService;

    [TestInitialize]
    public void Setup()
    {
        _authServiceMock = new Mock<IAuthService>();
        _authServiceMock.Setup(a => a.GetAccessToken()).Returns("test-token");

        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://xydzsl34r0.execute-api.us-east-1.amazonaws.com")
        };

        _restaurantService = new RestaurantService(_authServiceMock.Object, _httpClient);
    }

    [TestMethod]
    public async Task GetAllRestaurants_ReturnsListOfRestaurants()
    {
        // Arrange
        var restaurantsJson = "[{\"id\":\"d290f1ee-6c54-4b01-90e6-d701748f0851\",\"numberOfSeats\":50,\"address\":\"123 Main St\",\"name\":\"Test Restaurant\",\"openTime\":\"08:00:00\",\"closeTime\":\"22:00:00\"}]";
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(restaurantsJson)
            });

        // Act
        var result = await _restaurantService.GetAllRestaurants();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Test Restaurant", result[0].Name);
    }

    [TestMethod]
    public async Task GetAllRestaurants_ReturnsEmptyListOnFailure()
    {
        // Arrange
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act
        var result = await _restaurantService.GetAllRestaurants();

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task IsFavouriteForCurrentUser_ReturnsTrueIfFavourite()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        var favouritesJson = $"[\"{restaurantId}\"]";
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(favouritesJson)
            });

        // Act
        var result = await _restaurantService.IsFavouriteForCurrentUser(restaurantId);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task IsFavouriteForCurrentUser_ReturnsFalseOnFailure()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act
        var result = await _restaurantService.IsFavouriteForCurrentUser(restaurantId);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task AddNewFavourite_ReturnsSuccessIfAdded()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

        // Act
        var result = await _restaurantService.AddNewFavourite(restaurantId);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task AddNewFavourite_ReturnsFailureOnFailure()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act
        var result = await _restaurantService.AddNewFavourite(restaurantId);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Spróbuj jeszcze raz", result.Error);
    }

    [TestMethod]
    public async Task RemoveFavourite_ReturnsSuccessIfRemoved()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

        // Act
        var result = await _restaurantService.RemoveFavourite(restaurantId);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task RemoveFavourite_ReturnsFailureOnFailure()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act
        var result = await _restaurantService.RemoveFavourite(restaurantId);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Spróbuj jeszcze raz", result.Error);
    }
}