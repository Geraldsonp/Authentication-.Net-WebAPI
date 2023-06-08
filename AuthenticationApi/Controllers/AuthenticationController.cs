using Application.DTOS;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = Application.Interfaces.IAuthenticationService;

namespace AuthenticationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authenticationService.LoginAsync(loginDto.Email, loginDto.Password);
        
        return Ok(result);
    }
}