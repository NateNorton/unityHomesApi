namespace HomesApi.Dtos;

public class ArticleUpdateDto
{
    public string? Title { get; set; } = "";
    public string? Content { get; set; } = "";
    public string? ImageUrl { get; set; }
    public string? Author { get; set; }
    public string? Link { get; set; }
}
