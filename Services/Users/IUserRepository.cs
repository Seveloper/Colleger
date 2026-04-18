using Domain;

namespace Services.Users;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int id);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<int> CreateAsync(User user);
}
