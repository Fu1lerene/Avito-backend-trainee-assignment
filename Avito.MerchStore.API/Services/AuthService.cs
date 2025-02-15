using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Avito.MerchStore.Domain.Repositories.Models;
using Avito.MerchStore.Domain.Services;
using Avito.MerchStore.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Avito.MerchStore.API.Services;

public class AuthService : IAuthService
{
    private readonly MerchDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(MerchDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string?> Authenticate(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);

        if (user == null)
        {
            user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}