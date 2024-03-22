using HomesApi.Dtos;
using HomesApi.Models;

namespace HomesApi.Data.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly HomesDbContext _context;

    public ArticleRepository(HomesDbContext context)
    {
        _context = context;
    }

    public IQueryable<Article> GetAllArticles()
    {
        var query = _context.Articles.AsQueryable();
        return query;
    }

    public async Task<Article?> GetArticleByIdAsync(long articleID)
    {
        return await _context.Articles.FindAsync(articleID);
    }

    public async Task<Article> AddArticle(Article article)
    {
        if (article == null)
        {
            throw new ArgumentNullException(nameof(article));
        }

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<bool> UpdateArticleAsync(long articleId, ArticleUpdateDto articleDto)
    {
        var article = await _context.Articles.FindAsync(articleId);

        if (article == null)
        {
            return false;
        }

        article.Title = articleDto.Title ?? article.Title;
        article.Content = articleDto.Content ?? article.Content;
        article.Author = articleDto.Author ?? article.Author;
        article.Link = articleDto.Link ?? article.Link;
        article.ImageUrl = articleDto.ImageUrl ?? article.ImageUrl;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteArticleAsync(long articleId)
    {
        var article = await _context.Articles.FindAsync(articleId);

        if (article == null)
        {
            return false;
        }

        try
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public bool ArticleExists(long id)
    {
        return _context.Articles.Any(e => e.Id == id);
    }
}
