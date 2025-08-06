using GenericRepository;
using Losev.Domain.Entities;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository : IRepository<AppUser>
{
    Task<List<AppUser>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AppUser?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}