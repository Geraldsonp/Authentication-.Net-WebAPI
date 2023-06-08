using Microsoft.AspNetCore.Identity;

namespace AplicationTests.Mocks;

public class UserManagerMock<TUser> : UserManager<TUser> where TUser : class
{
    public UserManagerMock(IUserStore<TUser> store, IPasswordHasher<TUser> passwordHasher)
        : base(store, null, null, null, null, null, null, null, null)
    {
        PasswordHasher = passwordHasher;
    }
    

    public override Task<IdentityResult> CreateAsync(TUser user, string password)
    {

        return Task.FromResult(IdentityResult.Success);
    }
}
