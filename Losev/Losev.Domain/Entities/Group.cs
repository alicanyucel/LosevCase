using Losev.Domain.Abstractions;
using Losev.Domain.Enums;

namespace Losev.Domain.Entities;

public sealed class Group:Entity
{
    public string Name { get; set; } =default!;
    public GroupType GroupType { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}
