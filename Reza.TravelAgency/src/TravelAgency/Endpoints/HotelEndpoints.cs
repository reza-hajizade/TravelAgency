using System.Runtime.InteropServices;
using System.Xml.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Endpoints.Contracts;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Endpoints;

public static class HotelEndpoints
{
    public static IEndpointRouteBuilder MapHotelEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/Create", Create);
        app.MapGet("/Get", GetByName);
        app.MapPut("/Update", UpdatePrice);
        app.MapDelete("/Delete", DeleteHotel);

        return app;
    }


    public static async Task<IResult> Create(
        [AsParameters] TravelAgencyService service,
        CreateHotelRequest createHotel,
        IValidator<CreateHotelRequest> validator
        )
    {

        var validate = await validator.ValidateAsync(createHotel);
        if (!validate.IsValid)
        {
            var errors = validate.Errors.Select(e => e.ErrorMessage).ToList();
            return Results.BadRequest(errors);
        }

        var hasHotel=await service.Context.hotels.AnyAsync(p=>p.Name== createHotel.Name);
        if (hasHotel)
        {
            return Results.NotFound($"Hotel with name '{createHotel.Name}' is existing.");
        }

        var newHotel = Hotel.Create(
            createHotel.Name,
            createHotel.PhoneNumber,
            createHotel.Address,
            createHotel.City,
            createHotel.Description,
            createHotel.Stars,
            createHotel.PricePerNight,
            createHotel.HasWifi,
            createHotel.HasParking
            );

        if (newHotel is null)
        {
            return Results.NotFound();
        }

        service.Context.hotels.Add( newHotel );
        await service.Context.SaveChangesAsync();

        return Results.Ok(newHotel);
    }

    public static async Task<IResult> GetByName(
        [AsParameters] TravelAgencyService service,
        string Name
        )
    {

        var hotel = await service.Context.hotels.FirstOrDefaultAsync(p => EF.Functions.ILike(p.Name, Name));

        if (hotel is null)
        {
            return Results.NotFound($"Hotel with name '{Name}' not found.");
        }

        return Results.Ok(hotel);

    }

    public static async Task<IResult> UpdatePrice(
        [AsParameters] TravelAgencyService service,
        UpdateHotelRequest updateHotel,
        IValidator<UpdateHotelRequest> validator

        )
    {

        var validate=await validator.ValidateAsync(updateHotel);

        if (!validate.IsValid)
        {
            var errors = validate.Errors.Select(e => e.ErrorMessage).ToList();
            return Results.BadRequest(errors);
        }

        var hotel = await service.Context.hotels.FirstOrDefaultAsync(p => EF.Functions.ILike(p.Name, updateHotel.Name));

        if (hotel is null)
        {
            return Results.NotFound($"Hotel with name '{updateHotel.Name}' not found.");
        }

        hotel.UpdatePrice(updateHotel.PricePerNight);

        await service.Context.SaveChangesAsync();

        return Results.Ok(hotel);

        
    }


    public static async Task<IResult> DeleteHotel(
        [AsParameters] TravelAgencyService service,
        string Name
        )
    {
        var hotel = await service.Context.hotels.FirstOrDefaultAsync(p => EF.Functions.ILike(p.Name, Name));

        if (hotel is null)
        {
            return Results.NotFound($"Hotel with name '{Name}' not found.");
        }

        service.Context.hotels.Remove(hotel);
        await service.Context.SaveChangesAsync();

        return Results.Ok();   


    }
}

