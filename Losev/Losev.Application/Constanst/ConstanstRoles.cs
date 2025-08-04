public static class ConstantsRoles
{
    public static List<AppRole> GetRoles()
    {
        return new List<AppRole>
        {
            new AppRole
            {
                Id = Guid.Parse("5ed62427-ec28-46ce-be0e-376005fae043"),
                Name = "Admin",
                RoleName = "Admin",
                NormalizedName = "ADMIN",
                Description = "Full access to all system features, user management, settings, and reporting.",
                CreatedAt = DateTime.UtcNow,
                UserRoles = new List<AppUserRole>() 
            },
            new AppRole
            {
                Id = Guid.Parse("46fecbbe-79b3-4a0c-85d2-a7846d901272"),
                Name = "User",
                RoleName = "User",
                NormalizedName = "User",
                Description = "System-level operations and configurations.",
                CreatedAt = DateTime.UtcNow,
                UserRoles = new List<AppUserRole>() 
            }
        };
    }
}