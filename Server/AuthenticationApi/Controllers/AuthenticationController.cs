using Application.DTOS;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = Application.Interfaces.IAuthenticationService;

namespace AuthenticationApi.Controllers;

[ApiController]
[Route("api/")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserCreationHandler _userCreationHandler;

    public AuthenticationController(IAuthenticationService authenticationService, IUserCreationHandler userCreationHandler)
    {
        _userCreationHandler = userCreationHandler;
        _authenticationService = authenticationService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authenticationService.LoginAsync(loginDto.Email, loginDto.Password);
        
        return Ok(result);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] UserPostDto userDto)
    {
        var result = await _userCreationHandler.CreateUserAsync(userDto);
        return Ok(result);
    }
}