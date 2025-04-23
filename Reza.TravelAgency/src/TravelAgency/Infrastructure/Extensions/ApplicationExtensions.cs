using FluentValidation;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure.Configurations;
using TravelAgency.Infrastructure.Persistence.Seed;
using TravelAgency.Models;
using TravelAgency.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        builder.Services.AddAuthorization(option =>
        {
            option.AddPolicy("IsAdmin", policy =>
            {
                policy.RequireRole("Admin");
            });
           
        });

        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("Jwt"));

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    }



}

