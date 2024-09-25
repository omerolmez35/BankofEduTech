using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.Features.Queries.AppUser.LoginUser;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BankofEduTech.Infrastructure.Infrastructure.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(LoginUserQueryResponse result)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, result.FullName));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, result.UserID));
        
        claims.Add(new Claim(ClaimTypes.Surname, result.UserID));  foreach (var item in result.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, item));
        }
      
        
    
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e15871cf-a257-45d5-b654-84fd56ad8094"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
    issuer: _configuration["Token:Issuer"],  // Token'ı çıkaran
    audience: _configuration["Token:Audience"],  // Token'ı kullanacak olan
    claims: claims,
    expires: DateTime.Now.AddDays(30),
    signingCredentials: creds
);



        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
