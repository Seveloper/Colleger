using Domain;

namespace Services.Users;

public interface IUserService
{
    Task<User?> RegisterAsync(string username, string email, string password, int createdBy);
    Task<string?> AuthenticateAsync(string username, string password);
}
