namespace HomesApi.Dtos;

public class MessageCreateDto
{
    public long ReceipientId { get; set; }
    public string Content { get; set; } = "";
}
