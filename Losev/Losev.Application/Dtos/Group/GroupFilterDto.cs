using Losev.Domain.Enums;

namespace Losev.Application.Dtos.Group;

public sealed class GroupFilterDto
{
    public string? Name { get; set; }
    public GroupType? GroupType { get; set; }
    public Guid? AppUserId { get; set; }
    public bool? IsDeleted { get; set; }
}
