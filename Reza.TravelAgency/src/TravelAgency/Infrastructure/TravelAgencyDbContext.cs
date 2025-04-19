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


    }
}
