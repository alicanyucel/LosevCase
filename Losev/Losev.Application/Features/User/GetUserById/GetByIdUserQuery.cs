using Losev.Application.Dtos.User;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.GetUserById;
public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDto>>;
