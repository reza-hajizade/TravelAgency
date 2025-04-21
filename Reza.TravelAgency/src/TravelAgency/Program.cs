using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Endpoints;
using TravelAgency.Endpoints.Contracts;
using TravelAgency.Infrastructure;
using TravelAgency.Models;
using TravelAgency.Services;
using FluentValidation;
using System.Reflection;
using TravelAgency.Infrastructure.Configurations;
using TravelAgency.Infrastructure.Persistence.Seed;
using TravelAgency.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using(var scope=app.Services.CreateScope())
{
 
    var seeder=scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
    await seeder.SeedRoleAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGroup("/api/v1/register")
    .WithTags("Auth")
    .MapAuthEndpoints();

app.UseAuthorization();

app.Run();
