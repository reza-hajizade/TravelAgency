using Microsoft.AspNetCore.Identity;

namespace TravelAgency.Infrastructure.Persistence.Seed
{
    public class IdentityDataSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public IdentityDataSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }



        public async Task SeedRoleAsync()
        {
            var roles =new[]  { "User", "Admin" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
