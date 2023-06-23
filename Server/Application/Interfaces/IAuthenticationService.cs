using Application.DTOS;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    Task<TokenResponseDto> LoginAsync(string username, string password);
}