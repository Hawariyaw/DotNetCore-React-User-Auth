using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using server.Controllers;
using server.Services;
using server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class LoginControllerTest
{
    private readonly ILogger<LoginController> _logger;
    private readonly LoginController _controller;
    private readonly Mock<IUserService> mock;
    public LoginControllerTest(ILogger<LoginController> logger)
    {
        _logger = logger;
        mock = new Mock<IUserService>();
        _controller = new LoginController(_logger, mock.Object);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsOkResult()
    {
        // Act
        Login credential = new Login()
        {
            UserName = "hewipoul@gmail.com",
            Password = "hewi"
        };
        var okResult = _controller.Login(credential);
        // Assert
        Assert.IsType<OkObjectResult>(okResult);
    }
}