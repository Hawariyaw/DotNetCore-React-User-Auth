using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using server.Controllers;
using server.Services;
using server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class UserControllerTest
{
    private readonly ILogger<UserController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly UserController _controller;
    private readonly Mock<IUserService> mock;
    public UserControllerTest(ILogger<UserController> logger, IEmailSender emailSender)
    {
        _logger = logger;
        _emailSender = emailSender;
        mock = new Mock<IUserService>();
        _controller = new UserController(_logger, mock.Object, _emailSender);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsOkResult()
    {
        // Act
        var okResult = _controller.GetAll();
        // Assert
        Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void Get_WhenCalled_ReturnsAllItems()
    {
        // Act
        var okResult = _controller.GetAll().Result as OkObjectResult;
        // Assert
        var items = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(2, items.Count);
    }
}