using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspireBlazorApp.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AspireBlazorApp.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IConfiguration configuration) : Controller
{
    [HttpPost("login")]
    public ActionResult<LoginResponseModel> Login([FromBody] LoginModel loginModel)
    {
        if (loginModel is { Username: "Admin", Password: "Admin" } or { Username: "User", Password: "User" })
        {
            var token = GenerateJwtToken(loginModel.Username);
            return Ok(new LoginResponseModel { Token = token });
        }

        return null;
    }

    private string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, username == "Admin" ? "Admin" : "User"),
        };
        var secret = configuration.GetValue<string>("Jwt:Secret");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "stuartmillman",
            audience: "stuartmillman",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}