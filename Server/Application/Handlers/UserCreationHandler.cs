using Application.Domain;
using Application.DTOS;
using Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers;

public class UserCreationHandler : IUserCreationHandler
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public UserCreationHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<TokenResponseDto> CreateUserAsync(UserPostDto userDto)
    {
        var user = userDto.Adapt<User>();
        user.UserName = userDto.Name;

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            return new TokenResponseDto{
                Token = _tokenService.GenerateToken(user),
                Profile = user.Adapt<UserProfileDto>()
            };
        }
        else
        {
            //Todo:Trow ERROR Later
        }
        
        return new TokenResponseDto{
            Token = "Error",
            Profile = null
        };
    }
}