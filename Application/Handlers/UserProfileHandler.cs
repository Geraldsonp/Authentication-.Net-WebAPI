using Application.Domain;
using Application.DTOS;
using Application.Exceptions;
using Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers;

public class UserProfileHandler : IUserProfileHandler
{
    private readonly UserManager<User> _userManager;

    public UserProfileHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserProfileDto> GetUserProfileAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("User");
        }
        
        var userProfile = user.Adapt<UserProfileDto>();

        return userProfile;
    }
}
