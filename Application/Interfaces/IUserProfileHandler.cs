using Application.DTOS;

namespace Application.Interfaces;

public interface IUserProfileHandler
{
    public  Task<UserProfileDto> GetUserProfileAsync(string userId);
}