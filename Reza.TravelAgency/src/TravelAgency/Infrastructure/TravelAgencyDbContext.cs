using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Infrastructure
{
    public class TravelAgencyDbContext:IdentityDbContext<ApplicationUser>
    {



        public TravelAgencyDbContext(DbContextOptions<TravelAgencyDbContext> options) : base(options)
        {


        }

        public DbSet<Hotel> hotels { get; set; }

    }
}
