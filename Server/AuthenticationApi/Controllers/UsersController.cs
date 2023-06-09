﻿using System.Security.Claims;
using Application.DTOS;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserCreationHandler _userCreationHandler;
    private readonly IUserEditHandler _userEditHandler;
    private readonly IUserProfileHandler _userProfileHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsersController(IUserCreationHandler userCreationHandler, IUserEditHandler userEditHandler,
        IUserProfileHandler userProfileHandler, IHttpContextAccessor httpContextAccessor)
    {
        _userCreationHandler = userCreationHandler;
        _userEditHandler = userEditHandler;
        _userProfileHandler = userProfileHandler;
        _httpContextAccessor = httpContextAccessor;
    }
    
    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserProfileDto>> GetProfile()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userProfile = await _userProfileHandler.GetUserProfileAsync(userId);
        return Ok(userProfile);
            
    }
    
    [HttpPut("profile/{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserEditDto userDto)
    {
        var result = await _userEditHandler.EditUserAsync(userId, userDto);

        return Ok(result);
    }
}