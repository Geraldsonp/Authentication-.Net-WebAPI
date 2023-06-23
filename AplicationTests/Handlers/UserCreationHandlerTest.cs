using Application.Domain;
using Application.DTOS;
using Application.Handlers;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AplicationTests;

public class UserCreationHandlerTest
{
    private Mock<UserManager<User>> _userManagerMock;
    private Mock<ITokenService> _tokenServiceMock;

    public UserCreationHandlerTest()
    {
        var storeMock = new Mock<IUserStore<User>>();
        _userManagerMock = new  Mock<UserManager<User>>(storeMock.Object, null, null, null, null, null, null, null, null);
        _tokenServiceMock = new Mock<ITokenService>();
    }

    [Fact]
    public async Task CreateUserAsync_ValidUser_ReturnsSuccessResult()
    {
        // Arrange
        var userDto = new UserPostDto
        {
            Name = "John",
            Email = "john@example.com",
            Password = "P@ssw0rd"
        };

        var token = "mocked_token";
       
        _tokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<User>()))
            .Returns(token);
        _userManagerMock
            .Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var handler = new UserCreationHandler(_userManagerMock.Object, _tokenServiceMock.Object);

        // Act
        var result = await handler.CreateUserAsync(userDto);

        // Assert

        result.Token.Should().Be(token);
        result.Profile.Name.Should().Be(userDto.Name);
        result.Profile.Email.Should().Be(userDto.Email);
        _userManagerMock.Verify(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public async Task CreateUserAsync_FailedUserCreation_ReturnsFailureResult()
    {
        // Arrange
        var userDto = new UserPostDto
        {
            Name = "John",
            Email = "john@example.com",
            Password = "P@ssw0rd"
        };

        var token = "mocked_token";
        _tokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<User>()))
            .Returns(token);
        
        _userManagerMock
            .Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "CreationFailed" }));

        var handler = new UserCreationHandler(_userManagerMock.Object, _tokenServiceMock.Object);

        // Act
        var result = await handler.CreateUserAsync(userDto);

        // Assert
        result.Token.Should().Be("Error");
        _userManagerMock.Verify(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
    }

}