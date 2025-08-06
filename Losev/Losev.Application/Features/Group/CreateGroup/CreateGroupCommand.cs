using Losev.Domain.Enums;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.CreateGroup;


public sealed record CreateGroupCommand(
    string Name,
    GroupType GroupType,
    string PassName,
    string Url,
    string Password,
    Guid AppUserId
) : IRequest<Result<string>>;
