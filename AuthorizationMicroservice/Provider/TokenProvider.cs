using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthorizationMicroservice.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationMicroservice.Provider;

public class TokenProvider : ITokenProvider
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TokenProvider> _logger;

    public TokenProvider(ApplicationDbContext context, ILogger<TokenProvider> logger)
    {
        _logger = logger;
        _context = context;
    }

    public string GenerateJWTToken(User user)
    {
        _logger.LogInformation("GenerateJWTToken method executed");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, "Member"),
            new Claim("UserName", user.Username),
            new Claim("Password", user.Password)
        };

        var token = new JwtSecurityToken(
            issuer: "mySystem",
            audience: "myUsers",
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        var returnData = new JwtSecurityTokenHandler().WriteToken(token);

        return returnData;
    }

    public List<User> Users()
    {
        _logger.LogInformation("Users method executed");
        var users = _context.Users.ToList();
        return users;
    }
}
