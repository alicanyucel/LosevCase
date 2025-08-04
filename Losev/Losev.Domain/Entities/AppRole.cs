using Microsoft.AspNetCore.Identity;

public sealed class AppRole : IdentityRole<Guid>
{
    
    public required ICollection<AppUserRole> UserRoles { get; set; }
    public required string RoleName { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}