using Application.Domain;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AplicationTests.Services;

public class AuthenticationServiceTests
{
    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var username = "testuser";
        var password = "password123";
        var user = new User { UserName = username };

        var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        userManagerMock.Setup(m => m.FindByEmailAsync(username)).ReturnsAsync(user);
        userManagerMock.Setup(m => m.CheckPasswordAsync(user, password)).ReturnsAsync(true);

        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(m => m.GenerateToken(user)).Returns("generated-token");

        var authenticationService = new AuthenticationService(userManagerMock.Object, tokenServiceMock.Object);

        // Act
        var result = await authenticationService.LoginAsync(username, password);

        // Assert
        result.Should().Be("generated-token");
    }

    [Fact]
    public async Task LoginAsync_InvalidCredentials_ThrowsException()
    {
        // Arrange
        var email = "testuser@email.com";
        var password = "invalidpassword";

        var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        userManagerMock.Setup(m => m.FindByEmailAsync(email)).ReturnsAsync((User)null);

        var authenticationService = new AuthenticationService(userManagerMock.Object, Mock.Of<ITokenService>());

        // Act & Assert
        Func<Task> act = async () => await authenticationService.LoginAsync(email, password);

        await act.Should().ThrowAsync<DomainException>()
            .WithMessage("Invalid username or password.");
    }
}