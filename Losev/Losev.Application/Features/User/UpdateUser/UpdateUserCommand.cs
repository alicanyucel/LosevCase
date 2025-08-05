using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string? RefreshToken,
    DateTime? RefreshTokenExpires,
    string IpAddress,
    bool StatusSuccess,
    DateTime DateTime,
    bool IsDeleted,
    List<Guid>? GroupIds,
    string PasswordSalt,
    string Password
) : IRequest<Result<string>>;

