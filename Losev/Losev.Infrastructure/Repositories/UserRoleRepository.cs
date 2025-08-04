using GenericRepository;
using Losev.Domain.Repositories;
using Losev.Infrastructure.Context;

namespace Losev.Infrastructure.Repositories;

internal sealed class UserRoleRepository : Repository<AppUserRole, ApplicationDbContext>, IUserRoleRepository
{
    public UserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
