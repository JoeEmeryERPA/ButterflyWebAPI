using Microsoft.AspNetCore.Authorization; // Add this using directive
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ButterflyWebAPI.Controllers;

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