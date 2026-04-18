using Dapper;
using Domain;
using Services.Data;

namespace Services.Users;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _factory;

    public UserRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Username = @username AND Status = 1",
            new { username });
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var conn = _factory.Create();
        return await conn.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Id = @id AND Status = 1",
            new { id });
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        using var conn = _factory.Create();
        var count = await conn.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM Users WHERE Username = @username",
            new { username });
        return count > 0;
    }

    public async Task<int> CreateAsync(User user)
    {
        using var conn = _factory.Create();
        return await conn.ExecuteScalarAsync<int>(@"
            INSERT INTO Users (Status, Username, Email, PasswordHash, CreatedBy, CreatedDate)
            OUTPUT INSERTED.Id
            VALUES (@Status, @Username, @Email, @PasswordHash, @CreatedBy, @CreatedDate);",
            user);
    }
}
