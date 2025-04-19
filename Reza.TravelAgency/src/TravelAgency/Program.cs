using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure;
using TravelAgency.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TravelAgencyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SvcDbContext")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<TravelAgencyDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
