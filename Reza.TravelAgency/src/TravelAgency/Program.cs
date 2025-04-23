using TravelAgency.Endpoints;
using TravelAgency.Infrastructure.Persistence.Seed;
using TravelAgency.Infrastructure.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using(var scope=app.Services.CreateScope())
{
    var seeder=scope.ServiceProvider.GetRequiredService<IdentityDataSeeder>();
    await seeder.SeedRoleAsync();
    await seeder.SeedAdminUserAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapGroup("/api/v1/Auth")
    .WithTags("Auth")
    .MapAuthEndpoints();

app.MapGroup("/api/v1/hotel")
    .WithTags("Hotel")
    .RequireAuthorization("IsAdmin")
    .MapHotelEndpoints();


app.MapGet("/ping", () => "pong");

app.Run();
