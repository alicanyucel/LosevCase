using Losev.Domain.Abstractions;

namespace Losev.Domain.Entities;

public sealed class Group:Entity
{
    public string Name { get; set; } =default!;
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}
