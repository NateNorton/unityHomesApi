namespace HomesApi.Dots;

public partial class UserLoginConfirmationDto
{
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
}
