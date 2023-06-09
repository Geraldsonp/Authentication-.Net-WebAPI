using System.ComponentModel.DataAnnotations;

namespace Application.DTOS;

public class UserPostDto
{
    [Required]
    public string Name { get; set; }
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}