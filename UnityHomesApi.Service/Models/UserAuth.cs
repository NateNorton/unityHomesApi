namespace HomesApi.Models;

public class UserAuth
{
    public long Id { get; set; }
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public string Email { get; set; } = "";
}
