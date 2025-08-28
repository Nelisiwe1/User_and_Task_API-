using Microsoft.AspNetCore.Mvc;
using User_and_Task_API.Models;
using User_and_Task_API.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
 

namespace User_and_Task_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (request == null) return BadRequest("Invalid login request");

            var user = await _userRepo.GetByUsernameAsync(request.UserName);
            if (user == null) return Unauthorized("Invalid username or password");

           

// Hash input password
using var sha256 = SHA256.Create();
var hashedInput = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

// Convert stored password from Base64
var storedPasswordBytes = Convert.FromBase64String(user.Password!);

// Compare
if (!hashedInput.SequenceEqual(storedPasswordBytes))
    return Unauthorized("Invalid username or password");


            // Generate JWT
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
