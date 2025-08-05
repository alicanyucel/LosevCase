using Losev.Domain.Entities;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.GetAllUser;

public record GetAllUsersQuery() : IRequest<Result<List<AppUser>>>;
