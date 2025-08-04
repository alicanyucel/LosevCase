using MediatR;
using TS.Result;

namespace Losev.Application.Features.Role.RoleSync;

public sealed record RoleSyncCommand() : IRequest<Result<string>>;
