using Losev.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public sealed class AppUserRole : IdentityUserRole<Guid>
{
    public required AppUser User { get; set; }
    public required AppRole Role { get; set; }
}