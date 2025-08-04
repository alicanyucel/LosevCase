using GenericRepository;
using Losev.Domain.Entities;
using Losev.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Losev.Infrastructure.Context;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser,
    AppRole,
    Guid,
    IdentityUserClaim<Guid>,
    AppUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>,
    IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
        builder.Entity<Group>()
            .Property(g => g.GroupType)
            .HasConversion(
                v => v.Name,
                v => GroupType.FromName(v, false)  
            );
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Entity<AppUserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
    }
}
