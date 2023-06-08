using Application.DTOS;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IUserCreationHandler
{
    Task<string> CreateUserAsync(UserPostDto userDto);
}