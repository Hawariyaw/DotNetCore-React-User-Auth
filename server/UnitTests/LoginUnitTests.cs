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
    private readonly LoginController _controller;
    private readonly Mock<IUserService> mock;
    public LoginControllerTest()
    {
        mock = new Mock<IUserService>();
        _controller = new LoginController(mock.Object);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsOkResult()
    {
        // Act
        Login credential = new Login()
        {
            UserName = "elias@gmail.com",
            Password = "ela"
        };
        var okResult = this._controller.Login(credential);
        // Assert
        Assert.IsType<OkObjectResult>(okResult);
    }
}