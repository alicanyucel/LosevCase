using Losev.Application.Dtos.Group;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.GetByIdGroup;

public sealed record GetGroupByIdQuery(Guid Id) : IRequest<Result<GroupDto>>;
