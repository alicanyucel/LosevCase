namespace Losev.Application.Dtos.User;

public sealed class UserRoleDto
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = default!;
}
