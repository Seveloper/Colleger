using System.ComponentModel.DataAnnotations;

namespace Domain;

public class User : Entity
{
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(75)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(200)]
    public string PasswordHash { get; set; } = string.Empty;
}
