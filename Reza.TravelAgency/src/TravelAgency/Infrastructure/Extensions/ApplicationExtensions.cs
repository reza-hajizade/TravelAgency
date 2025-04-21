using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure.Configurations;
using TravelAgency.Infrastructure.Persistence.Seed;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Infrastructure.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TravelAgencyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SvcDbContext")));


        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<TravelAgencyDbContext>();

        builder.Services.AddScoped<TravelAgencyService>();
        builder.Services.AddScoped<JwtService>();
        builder.Services.AddScoped<IdentityDataSeeder>();
        builder.Services.AddAuthorization();

        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("Jwt"));

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    }



}

