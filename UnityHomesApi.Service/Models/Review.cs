namespace HomesApi.Models;

public class Review
{
    public long Id { get; set; }
    public long PropertyId { get; set; }
    public Property? Property { get; set; }
    public long ReviewerId { get; set; }
    public User? Reviewer { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
