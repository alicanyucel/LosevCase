using MediatR;

namespace Losev.Application.Features.Role.GetRoles;

public sealed record GetRolesQuery : IRequest<List<AppRole>>;
