using Microsoft.AspNetCore.Identity;

namespace Losev.Domain.Entities;

public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public string IpAddress { get; set; } = default!;
    public bool StatusSuccess { get; set; } =false;
    public DateTime DateTime { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public string PasswordSalt { get; set; } = default!;    

}
