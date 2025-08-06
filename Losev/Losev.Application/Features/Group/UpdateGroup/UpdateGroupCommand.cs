using Losev.Domain.Enums;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.UpdateGroup;

public sealed record UpdateGroupCommand(
 Guid Id,
 string Name,
 GroupType GroupType,
 string PassName,
 string Url,
 string Password
) : IRequest<Result<string>>;
