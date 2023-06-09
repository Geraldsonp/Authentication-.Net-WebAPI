using Application.Domain;
using Application.DTOS;
using Application.Exceptions;
using Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers;

public class UserEditHandler : IUserEditHandler
{
    private readonly UserManager<User> _userManager;

    public UserEditHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityResult> EditUserAsync(string userId, UserEditDto userDto)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("User");
        }

        userDto.Adapt(user);

        var result = await _userManager.UpdateAsync(user);

        return result;
    }
}