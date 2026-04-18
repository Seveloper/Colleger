using System.Net;
using Asp.Versioning;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Services.Users;

namespace Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController(IUserService users) : ControllerBase
{
    private readonly IUserService _users = users;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _users.AuthenticateAsync(request.Username, request.Password);
        if (token is null)
            return Unauthorized(ApiResponse<LoginResponse>.Fail(HttpStatusCode.Unauthorized, "Invalid username or password."));
        return Ok(ApiResponse<LoginResponse>.Ok(new LoginResponse(token)));
    }
}

public record LoginRequest(string Username, string Password);
public record LoginResponse(string AccessToken);
