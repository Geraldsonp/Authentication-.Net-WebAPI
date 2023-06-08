namespace Application.Interfaces;

public interface IAuthenticationService
{
    Task<string> LoginAsync(string username, string password);
}