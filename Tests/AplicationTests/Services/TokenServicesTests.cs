
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Application.Domain;
using Application.Services;

namespace AplicationTests.Services;

public class TokenServiceTests
{
    
    [Fact]
    public void GenerateToken_ReturnsValidToken()
    {
        // Arrange
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["JwtSettings:SecretKey"]).Returns("mySuper_secret_key");
        configurationMock.Setup(c => c["JwtSettings:Audience"]).Returns("your_audience");

        var tokenService = new TokenService(configurationMock.Object);
        var user = new User
        {
            UserName = "testuser",
            Email = "testuser@example.com"
        };

        // Act
        var token = tokenService.GenerateToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("mySuper_secret_key");
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateAudience = false,
            ValidateIssuer = false
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        Assert.NotNull(principal);

        var identity = principal.Identity as ClaimsIdentity;
        Assert.NotNull(identity);

        var nameClaim = identity.FindFirst(ClaimTypes.Name);
        Assert.NotNull(nameClaim);
        Assert.Equal("testuser", nameClaim.Value);

        var emailClaim = identity.FindFirst(ClaimTypes.Email);
        Assert.NotNull(emailClaim);
        Assert.Equal("testuser@example.com", emailClaim.Value);
    }
    
    [Fact]
    public void GenerateToken_WithNullUser_ReturnsNullToken()
    {
        // Arrange
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["JwtSettings:SecretKey"]).Returns("your_secret_key");
        configurationMock.Setup(c => c["JwtSettings:Issuer"]).Returns("your_issuer");
        configurationMock.Setup(c => c["JwtSettings:Audience"]).Returns("your_audience");

        var tokenService = new TokenService(configurationMock.Object);
        User user = null;

        // Act
        var token = tokenService.GenerateToken(user);

        // Assert
        Assert.Null(token);
    }

}
