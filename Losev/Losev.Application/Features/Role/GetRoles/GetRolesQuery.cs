using MediatR;

namespace Losev.Application.Features.Role.GetRoles;

public class GetRolesQuery : IRequest<List<AppRole>> { }
