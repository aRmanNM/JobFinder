using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobFinder.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobFinder.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> Signup(UserAuthModel model)
    {
        var user = new AppUser
        {
            UserName = model.UserName,
            JoinedAt = DateTimeOffset.Now,
            SearchCount = 5,
        };

        var res = await _userManager.CreateAsync(user, model.Password);
        if (!res.Succeeded)
            return BadRequest(res.Errors);

        return Ok(new
        {
            token = GenerateToken(user),
        });
    }

    [HttpPost("Signin")]
    public async Task<IActionResult> Signin(UserAuthModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user == null)
            return NotFound();

        var passValid = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!passValid)
            return BadRequest();

        return Ok(new
        {
            token = GenerateToken(user),
        });
    }

    private string GenerateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new Exception("jwt key not set")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}