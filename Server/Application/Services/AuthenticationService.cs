using Application.Domain;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthenticationService(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new BadRequestException("Invalid username or password.");
        }

        var token = _tokenService.GenerateToken(user);
        
        return token;
    }
}
