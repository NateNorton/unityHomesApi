using HomesApi.Data;
using HomesApi.Data.Repositories;
using HomesApi.Dtos;
using HomesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace unityHomesApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;

        public ArticleController(
            IUserRepository userRepository,
            IArticleRepository articleRepository,
            HomesDbContext context
        )
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            var articles = await _articleRepository.GetAllArticles().ToListAsync();

            if (articles == null)
            {
                return NotFound();
            }

            return articles;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(long id)
        {
            var article = await _articleRepository.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(long id, ArticleUpdateDto article)
        {
            var updated = await _articleRepository.UpdateArticleAsync(id, article);

            if (!updated)
            {
                return NotFound();
            }

            return Ok("Article updated successfully");
        }

        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            try
            {
                var addedArticle = await _articleRepository.AddArticle(article);
                return Ok(addedArticle);
            }
            catch (Exception e)
            {
                return StatusCode(500, "An error occured while attempting to create the article.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(long id)
        {
            try
            {
                var deleted = await _articleRepository.DeleteArticleAsync(id);
                return Ok("Article deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while attempting to delete the article.");
            }
        }
    }
}
