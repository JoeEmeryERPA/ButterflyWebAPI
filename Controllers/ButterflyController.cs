using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ButterflyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protect all endpoints in this controller
    public class ButterflyWebAPIController : ControllerBase
    {
        private readonly ILogger<ButterflyWebAPIController> _logger;

        public ButterflyWebAPIController(ILogger<ButterflyWebAPIController> logger)
        {
            _logger = logger;
        }

        // Endpoint to generate JWT token
        [HttpPost("token")]
        [AllowAnonymous] // Allow anonymous access to token endpoint
        public ActionResult<string> GenerateToken([FromBody] LoginModel login)
        {
            // Simple validation for demonstration purposes
            if (login.Username == "user" && login.Password == "password") // Replace with real validation
            {
                var token = CreateToken(login.Username);
                return Ok(token);
            }

            return Unauthorized();
        }

private string CreateToken(string username)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    // Retrieve the key from configuration, ensuring it is at least 32 bytes
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourVeryStrongSuperSecretKeyThatIsAtLeast32CharactersLong")); // Make sure to use a strong key here
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "YourIssuer",
        audience: "YourAudience",
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}


        [HttpGet("add")]
        public ActionResult<double> Add(double num1, double num2)
        {
            _logger.LogInformation($"Adding {num1} and {num2}");
            return Ok(num1 + num2);
        }

        [HttpGet("subtract")]
        public ActionResult<double> Subtract(double num1, double num2)
        {
            _logger.LogInformation($"Subtracting {num1} from {num2}");
            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public ActionResult<double> Multiply(double num1, double num2)
        {
            _logger.LogInformation($"Multiplying {num1} and {num2}");
            return Ok(num1 * num2);
        }

        [HttpGet("divide")]
        public ActionResult<double> Divide(double num1, double num2)
        {
            _logger.LogInformation($"Dividing {num1} by {num2}");
            if (num2 == 0)
            {
                _logger.LogWarning("Attempted division by zero");
                return BadRequest("Cannot divide by zero.");
            }
            return Ok(num1 / num2);
        }
    }

    // Model for Login
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
