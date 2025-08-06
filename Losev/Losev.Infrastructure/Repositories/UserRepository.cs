using GenericRepository;
using Losev.Domain.Entities;
using Losev.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Losev.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<AppUser, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<AppUser>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await GetAll().ToListAsync(cancellationToken);
    }

    public async Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAll().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}
