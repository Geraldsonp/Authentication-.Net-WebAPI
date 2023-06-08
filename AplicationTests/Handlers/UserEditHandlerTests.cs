using System.Threading.Tasks;
using Application.Domain;
using Application.DTOS;
using Application.Exceptions;
using Application.Handlers;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AplicationTests;
public class UserEditHandlerTests
{
    [Fact]
    public async Task EditUserAsync_ValidUserId_ReturnsIdentityResult()
    {
        // Arrange
        var userId = "user1";
        var userDto = new UserEditDto
        {
            Name = "John Doe",
            Bio = "randoms wordss"
        };

        var user = new User { Id = userId };
        var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        userManagerMock.Setup(u => u.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

        var userEditHandler = new UserEditHandler(userManagerMock.Object);

        // Act
        var result = await userEditHandler.EditUserAsync(userId, userDto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        userManagerMock.Verify(u => u.FindByIdAsync(userId), Times.Once);
        userManagerMock.Verify(u => u.UpdateAsync(user), Times.Once);
    }

    [Fact]
    public async Task EditUserAsync_InvalidUserId_ThrowsNotFoundException()
    {
        // Arrange
        var userId = "user1";
        var userDto = new UserEditDto
        {
            Name = "John Doe",
            Bio = "randoms wordss"
        };

        var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync((User)null);

        var userEditHandler = new UserEditHandler(userManagerMock.Object);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => userEditHandler.EditUserAsync(userId, userDto));
        userManagerMock.Verify(u => u.FindByIdAsync(userId), Times.Once);
        userManagerMock.Verify(u => u.UpdateAsync(It.IsAny<User>()), Times.Never);
    }
}
