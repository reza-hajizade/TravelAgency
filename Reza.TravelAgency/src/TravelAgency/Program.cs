using TravelAgency.Endpoints;
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

app.MapGroup("/api/v1/hotel")
    .WithTags("Hotel")
    .MapHotelEndpoints();

app.UseAuthorization();

app.Run();
