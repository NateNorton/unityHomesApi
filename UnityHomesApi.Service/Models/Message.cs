namespace HomesApi.Models;

public class Message
{
    public long Id { get; set; }
    public string Content { get; set; } = "";
    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public User? Sender { get; set; }
    public User? Receiver { get; set; }
    public DateTime DateSent { get; set; }
    public bool Edited { get; set; }
}
