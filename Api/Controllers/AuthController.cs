using Microsoft.AspNetCore.Mvc;
using Services.Auth;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        // TODO: replace with real user/credential lookup once users are modeled.
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return Unauthorized();

        var token = _jwtService.GenerateToken(userId: "1", userName: request.Username);
        return Ok(new LoginResponse(token));
    }
}

public record LoginRequest(string Username, string Password);
public record LoginResponse(string AccessToken);
