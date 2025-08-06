using Losev.Domain.Enums;

namespace Losev.Application.Dtos.Group;

public sealed record GroupDto(
    Guid Id,
    string Name,
    GroupType GroupType,
    string PassName,
    string Url,
    Guid AppUserId
);
