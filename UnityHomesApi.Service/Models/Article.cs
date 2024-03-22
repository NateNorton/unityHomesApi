namespace HomesApi.Models;

public class Article
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public string Author { get; set; } = "";
    public string? Link { get; set; }
    public string? ImageUrl { get; set; }
}
