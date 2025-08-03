using Microsoft.AspNetCore.Identity;

namespace Losev.Application.Services;

public interface IPasswordHasher<TUser>
{
    string HashPassword(TUser user, string password);
    PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword);
}
