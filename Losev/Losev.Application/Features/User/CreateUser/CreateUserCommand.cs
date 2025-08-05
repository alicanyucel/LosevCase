using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.CreateUser;
public record CreateUserCommand(
    string FirstName,
    string LastName,
    string IpAddress,
    bool StatusSuccess,
    DateTime DateTime,
    bool IsDeleted,
    List<Guid>? GroupIds,
    string Password
) : IRequest<Result<string>>;

