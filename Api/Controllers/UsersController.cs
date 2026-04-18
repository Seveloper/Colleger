using System.Net;
using System.Security.Claims;
using Asp.Versioning;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Services.Users;

namespace Api.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController(IUserService users) : ControllerBase
{
    private readonly IUserService _users = users;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest(ApiResponse<object>.Fail(HttpStatusCode.BadRequest, "Username and password are required."));

        var createdBy = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : 0;
        var user = await _users.RegisterAsync(request.Username, request.Email, request.Password, createdBy);
        if (user is null)
            return Conflict(ApiResponse<object>.Fail(HttpStatusCode.Conflict, "Username already exists."));

        return Created($"/api/v1/users/{user.Id}",
            ApiResponse<object>.Ok(new { user.Id, user.Username, user.Email }));
    }
}

public record CreateUserRequest(string Username, string Email, string Password);
