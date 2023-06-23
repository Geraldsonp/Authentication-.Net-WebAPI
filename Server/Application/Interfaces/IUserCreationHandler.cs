using Application.DTOS;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IUserCreationHandler
{
    Task<TokenResponseDto> CreateUserAsync(UserPostDto userDto);
}