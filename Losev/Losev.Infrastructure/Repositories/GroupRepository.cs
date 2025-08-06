using GenericRepository;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using Losev.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Losev.Infrastructure.Repositories;

internal sealed class GroupRepository : Repository<Group, ApplicationDbContext>, IGroupRepository
{
    public GroupRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<Group?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAll().FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<List<Group>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GetAll().ToListAsync(cancellationToken);
    }
}
