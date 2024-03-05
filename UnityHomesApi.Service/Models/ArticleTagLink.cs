namespace HomesApi.Models;

public class ArticleTagLink
{
    public long Id { get; set; }
    public long ArticleId { get; set; }
    public Article? Article { get; set; }
    public long TagId { get; set; }
    public Tag? Tag { get; set; }
}
