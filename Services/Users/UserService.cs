using Domain;
using Services.Auth;

namespace Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IJwtService _jwt;

    public UserService(IUserRepository repo, IJwtService jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    public async Task<User?> RegisterAsync(string username, string email, string password, int createdBy)
    {
        if (await _repo.ExistsByUsernameAsync(username))
            return null;

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Status = Status.Active,
            CreatedBy = createdBy,
            CreatedDate = DateTime.UtcNow
        };

        user.Id = await _repo.CreateAsync(user);
        return user;
    }

    public async Task<string?> AuthenticateAsync(string username, string password)
    {
        var user = await _repo.GetByUsernameAsync(username);
        if (user is null) return null;
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;

        return _jwt.GenerateToken(user.Id.ToString(), user.Username);
    }
}
