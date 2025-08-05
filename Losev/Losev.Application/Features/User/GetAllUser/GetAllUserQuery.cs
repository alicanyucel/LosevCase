using Losev.Application.Dtos.User;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.GetAllUser;

public record GetAllUsersQuery() : IRequest<Result<List<UserDto>>>;
