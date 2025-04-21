using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Endpoints.Contracts;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/register", Register);
        app.MapPost("/login", Login);

        return app;
    }

    public static async Task<IResult> Register(
       [AsParameters] TravelAgencyService service,
         RegisterUserRequest register,
         UserManager<ApplicationUser> userManager,
         IValidator<RegisterUserRequest> validator,
         JwtService jwtService

        )
    {

        var validate = await validator.ValidateAsync(register);
        if(!validate.IsValid)
        {
            var errors = validate.Errors.Select(e => e.ErrorMessage).ToList();
            return Results.BadRequest(errors);
        }


        var existingUser = await userManager.FindByEmailAsync(register.Email);
        if(existingUser !=null)
        {
            return Results.BadRequest("This User Already Exist");
        }

        var newUser = new ApplicationUser
        {
            UserName = register.Email,
            Email = register.Email,
            FullName = register.FullName
        };

        var result=await userManager.CreateAsync(newUser,register.Password);

        if (!result.Succeeded)
        {
            var errors=result.Errors.Select(e=>e.Description).ToList();
            return Results.BadRequest(errors);

        }

        await userManager.AddToRoleAsync(newUser, "User");

        var token=await jwtService.Generate(newUser);


        return Results.Ok(token);

    }



    public static async Task<IResult> Login(
        [AsParameters] TravelAgencyService service,
        LoginUserRequest login,
        IValidator<LoginUserRequest> validator,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
         JwtService jwtService
        )
    {

        var validate = await validator.ValidateAsync(login);
        if (!validate.IsValid)
        {
            var errors = validate.Errors.Select(e => e.ErrorMessage).ToList();
            return Results.BadRequest(errors);
        }

        var existUser = await userManager.FindByEmailAsync(login.username);

        if (existUser is null)      
            return Results.BadRequest("User Not Found");
        

        var result = await signInManager.CheckPasswordSignInAsync(existUser, login.password, false);

        if (!result.Succeeded)       
            return Results.BadRequest("Invalid Credentioal");
        

        var token=await jwtService.Generate(existUser);

        return Results.Ok(token);   

    }
}

