using Application.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Application.DAO;

public class ApiDbContext : IdentityDbContext<User>
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        Database.Migrate();
    }
    
    
}