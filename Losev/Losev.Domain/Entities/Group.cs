using Losev.Domain.Abstractions;
using Losev.Domain.Enums;

namespace Losev.Domain.Entities;

public sealed class Group:Entity
{
    public string Name { get; set; } =default!;
    public required GroupType GroupType { get; set; }
    public string PassName { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}
