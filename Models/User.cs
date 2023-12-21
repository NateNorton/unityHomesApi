namespace HomesApi.Models
{
  public class User
  {
    public long Id { get; set; }
    public string? userName { get; set; }
    public string? email { get; set; }
    public string? passwordHash { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? phoneNumber { get; set; }
    public bool IsAvailable { get; set; }
  }
}