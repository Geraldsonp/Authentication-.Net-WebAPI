using Application.DTOS;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IUserEditHandler
{
    public Task<IdentityResult> EditUserAsync(string userId, UserEditDto userDto);
}