using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Losev.Infrastructure.Context;

internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-L6NJT48\\SQLEXPRESS;Initial Catalog=LosevDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
