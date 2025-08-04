using GenericRepository;
using Losev.Domain.Entities;
using Losev.Infrastructure.Context;

namespace Losev.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<AppUser, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

}
