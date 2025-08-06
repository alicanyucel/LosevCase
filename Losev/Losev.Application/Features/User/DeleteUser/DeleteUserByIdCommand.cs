using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.DeleteUser;

public record DeleteGroupByIdCommand(Guid UserId) : IRequest<Result<string>>;
