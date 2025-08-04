using Losev.Application.Features.Role.GetRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;

public class GetRolesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnListOfRoles()
    {
        var roles = new List<AppRole>
        {
            new AppRole
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                RoleName = "Admin",
                UserRoles = new List<AppUserRole>(),
                CreatedAt = DateTime.UtcNow
            },
            new AppRole
            {
                Id = Guid.NewGuid(),
                Name = "User",
                RoleName = "User",
                UserRoles = new List<AppUserRole>(),
                CreatedAt = DateTime.UtcNow
            }
        }.AsQueryable();

        var fakeRoleManager = new FakeRoleManager(roles);
        var handler = new GetRolesQueryHandler(fakeRoleManager);

        var result = await handler.Handle(new GetRolesQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.Name == "Admin");
        Assert.Contains(result, r => r.Name == "User");
    }
}

public class FakeRoleManager : RoleManager<AppRole>
{
    private readonly IQueryable<AppRole> _roles;

    public FakeRoleManager(IQueryable<AppRole> roles)
       : base(new Mock<IRoleStore<AppRole>>().Object,
              new IRoleValidator<AppRole>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<ILogger<RoleManager<AppRole>>>().Object)
    {
        _roles = roles;
    }

    public override IQueryable<AppRole> Roles => _roles;
}