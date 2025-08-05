using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.DeleteUser;

public record DeleteUserByIdCommand(Guid UserId) : IRequest<Result<string>>;
