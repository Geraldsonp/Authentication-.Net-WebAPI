namespace Application.DTOS;

public class UserProfileDto
{
    public string Id { get; internal set; }
    public string Name { get; internal set; }
    public string Email { get; internal set; }
    public string? Bio { get; internal set; }
    public string? ImageUrl { get; internal set; }
}