namespace Losev.Test.Features.Roles;

public class RoleSyncCommandHandlerTests
{
    [Fact]
    public void GetRoles_ShouldReturnAllPredefinedRoles()
    {
        var roles = ConstantsRoles.GetRoles();
        Assert.NotNull(roles);
        Assert.True(roles.Count > 0);
        var adminRole = roles.FirstOrDefault(r => r.Name == "Admin");
        Assert.NotNull(adminRole);
        Assert.Equal("ADMIN", adminRole.NormalizedName);
    }
}
