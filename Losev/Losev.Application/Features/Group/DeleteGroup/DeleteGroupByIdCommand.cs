using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.DeleteGroup;

public sealed record DeleteGroupCommand(Guid Id) : IRequest<Result<string>>;
