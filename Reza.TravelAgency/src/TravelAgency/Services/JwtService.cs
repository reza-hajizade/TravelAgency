using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelAgency.Infrastructure.Configurations;
using TravelAgency.Models;

namespace TravelAgency.Services;

public class JwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public JwtService(IOptions<JwtSettings> jwtSettings,UserManager<ApplicationUser> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
    }


    public async Task<string> Generate(ApplicationUser user)
    {
        var Claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.FullName),
            new Claim(ClaimTypes.Email,user.Email)
        };


        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            Claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer : _jwtSettings.Issuer,
            claims : Claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
            signingCredentials: signingCredentials
            );
            

        return new JwtSecurityTokenHandler().WriteToken(token); 

    }

}

