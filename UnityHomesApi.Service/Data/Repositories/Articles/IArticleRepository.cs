using HomesApi.Dtos;
using HomesApi.Models;

namespace HomesApi.Data.Repositories;

public interface IArticleRepository
{
    IQueryable<Article> GetAllArticles();
    Task<Article?> GetArticleByIdAsync(long id);
    Task<Article> AddArticle(Article article);
    Task<bool> UpdateArticleAsync(long articleId, ArticleUpdateDto articleDto);
    Task<bool> DeleteArticleAsync(long id);
    bool ArticleExists(long id);
}
