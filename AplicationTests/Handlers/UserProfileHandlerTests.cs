using System.Threading.Tasks;
using Application.Domain;
using Application.Exceptions;
using Application.Handlers;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AplicationTests;
public class UserProfileHandlerTests
{
    [Fact]
    public async Task GetUserProfileAsync_ValidUserId_ReturnsUserProfileDto()
    {
        // Arrange
        var userId = "user1";
        var user = new User { Id = userId, Name = "john.doe" };
        var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);

        var userProfileHandler = new UserProfileHandler(userManagerMock.Object);

        // Act
        var result = await userProfileHandler.GetUserProfileAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
        Assert.Equal(user.Name, result.Name);
        userManagerMock.Verify(u => u.FindByIdAsync(userId), Times.Once);
    }

    [Fact]
    public async Task GetUserProfileAsync_InvalidUserId_ThrowsNotFoundException()
    {
        // Arrange
        var userId = "user1";
        var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync((User)null);

        var userProfileHandler = new UserProfileHandler(userManagerMock.Object);

        // Act and Assert
        await Assert.ThrowsAsync<NotFoundException>(() => userProfileHandler.GetUserProfileAsync(userId));
        userManagerMock.Verify(u => u.FindByIdAsync(userId), Times.Once);
    }
}
