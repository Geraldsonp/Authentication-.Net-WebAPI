using Application.DTOS;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IUserEditHandler
{
    public Task<UserProfileDto> EditUserAsync(string userId, UserEditDto userDto);
}