using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using stshadowbackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace stshadowbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("test-null-reference")]
        public IActionResult TestNullReference()
        {
            string nullObject = null;
            var length = nullObject.Length; // This will throw NullReferenceException
            return Ok(length);
        }

        [HttpGet("test-custom-exception")]
        public IActionResult TestCustomException()
        {
            throw new InvalidOperationException("This is a custom exception for testing.");
        }

        [HttpGet("test-argument-exception")]
        public IActionResult TestArgumentException()
        {
            throw new ArgumentException("Invalid argument provided for testing.");
        }





        [HttpPost("login")]
        public IActionResult Login([FromBody] User? user, [FromQuery] string? username, [FromQuery] string? password)
        {
            user.Role ??= "Viewer"; // Assign default role if none is provided

            if (user == null && (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
            {
                return BadRequest("A non-empty request body or query parameters are required.");
            }

            var loginUser = user ?? new User { Username = username, Password = password };

            // Replace with a proper user store or database
            if (loginUser.Username == "admin" && loginUser.Password == "password")
            {
                var token = GenerateJwtToken(loginUser.Username, "Admin");
                return Ok(new { Token = token });
            }
            else if (loginUser.Username == "editor" && loginUser.Password == "password")
            {
                var token = GenerateJwtToken(loginUser.Username, "Editor");
                return Ok(new { Token = token });
            }
            else if (loginUser.Username == "viewer" && loginUser.Password == "password")
            {
                var token = GenerateJwtToken(loginUser.Username, "Viewer");
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }


        private string GenerateJwtToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
