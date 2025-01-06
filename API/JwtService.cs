using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MusicStore.API.Models;

namespace MusicStore.API;

public class JwtService
{
    public string CreateToken(User user)
    {
        JwtSecurityTokenHandler handler = new();
        
        byte[] privateKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!);
        SigningCredentials credentials = new(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);
        
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddDays(1),
            Subject = GenerateClaims(user)
        };
          
        SecurityToken? token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
  
    // Specifies what info will be contained within the token
    private static ClaimsIdentity GenerateClaims(User user)
    {
        ClaimsIdentity ci = new();
  
        ci.AddClaim(new Claim("id", user.Id!));
        ci.AddClaim(new Claim("email", user.Email));
        ci.AddClaim(new Claim("admin", user.IsAdmin.ToString().ToLower()));
          
        return ci;
    }
}