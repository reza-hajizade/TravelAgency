using System.Transactions;
using Microsoft.AspNetCore.Identity;
using TravelAgency.Models;

namespace TravelAgency.Infrastructure.Persistence.Seed
{
    public class IdentityDataSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityDataSeeder(RoleManager<IdentityRole> roleManager
            , UserManager<ApplicationUser> userManager
            , IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
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


        public async Task SeedAdminUserAsync()
        {

            var adminEmail = _configuration["Admin:Email"];
            var adminPassword = _configuration["Admin:Password"];

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new Exception("Admin credentials are not set in configuration");
            }

            var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin != null)
            {
                Console.WriteLine("Admin already exists");
                return;
            }


            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Super Admin"
            };

            var result = await _userManager.CreateAsync(admin, adminPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create admin user");
            }


            await _userManager.AddToRoleAsync(admin, "Admin");

        }
    }
}
