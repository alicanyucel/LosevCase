using Losev.Application.Dtos.Group;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.GetAllGroup;

public sealed record GetAllGroupsQuery : IRequest<Result<List<GroupDto>>>;

