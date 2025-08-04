using GenericRepository;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using Losev.Infrastructure.Context;

namespace Losev.Infrastructure.Repositories;

internal sealed class GroupRepository : Repository<Group, ApplicationDbContext>, IGroupRepository
{
    public GroupRepository(ApplicationDbContext context) : base(context)
    {

    }

}
