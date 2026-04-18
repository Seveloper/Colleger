using Microsoft.AspNetCore.Mvc;
using Services.Users;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _users;

    public AuthController(IUserService users)
    {
        _users = users;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var token = await _users.AuthenticateAsync(request.Username, request.Password);
        if (token is null) return Unauthorized();
        return Ok(new LoginResponse(token));
    }
}

public record LoginRequest(string Username, string Password);
public record LoginResponse(string AccessToken);
