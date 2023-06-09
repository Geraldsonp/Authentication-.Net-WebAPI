using Microsoft.AspNetCore.Identity;

namespace Application.Domain;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}