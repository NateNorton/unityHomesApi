namespace HomesApi.Dtos;

public partial class UserLoginConfirmationDto
{
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
}
