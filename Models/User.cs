namespace eshop_auth.Models;

public class User
{
    public int Id { get; init; }
    public required string  Email { get; init; }
    public required string? Password { get; init; }
    public string Username { get; set; } = null!;
}