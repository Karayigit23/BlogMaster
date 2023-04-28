using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogMaster.Controller;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    
    private readonly IArticleRepository _articleRepository;

    // Inject the ArticleRepository via constructor injection
    public ArticleController(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    // GET api/article
    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAllArticles()
    {
        var articles = await _articleRepository.GetAllArticles();
        return Ok(articles);
    }

    // GET api/article/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticleById(int id)
    {
        var article = await _articleRepository.GetArticleById(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }

    // GET api/article/search?id=1&keyword=test&categoryId=2&tagId=3
    [HttpGet("search")]
    public async Task<ActionResult<List<Article>>> Search(int? id, string? keyword, int? categoryId, int? tagId)
    {
        var articles = await _articleRepository.Search(id, keyword, categoryId, tagId);
        return Ok(articles);
    }

    // GET api/article/category/categoryName
    [HttpGet("category/{category}")]
    public async Task<ActionResult<List<Article>>> GetArticlesByCategory(int category)
    {
        var articles = await _articleRepository.GetArticlesByCategory(category);
        return Ok(articles);
    }

    // GET api/article/tag/tagName
    [HttpGet("tag/{tag}")]
    public async Task<ActionResult<List<Article>>> GetArticlesByTag(string tag)
    {
        var articles = await _articleRepository.GetArticlesByTag(tag);
        return Ok(articles);
    }

    // GET api/article/author/authorName
    [HttpGet("author/{author}")]
    public async Task<ActionResult<List<Article>>> GetArticlesByAuthor(string author)
    {
        var articles = await _articleRepository.GetArticlesByAuthor(author);
        return Ok(articles);
    }

    // POST api/article
    [HttpPost]
    public async Task<ActionResult> AddArticle(Article article)
    {
        await _articleRepository.AddArticle(article);
        return Ok();
    }

    // PUT api/article/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateArticle(int id, Article article)
    {
        if (id != article.Id)
        {
            return BadRequest();
        }
        await _articleRepository.UpdateArticle(article);
        return Ok();
    }

    // DELETE api/article/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteArticle(int id)
    {
        var article = await _articleRepository.GetArticleById(id);
        if (article == null)
        {
            return NotFound();
        }
        await _articleRepository.DeleteArticle(article);
        return Ok();
    }

    // GET api/article/author/authorName/count
    [HttpGet("author/{author}/count")]
    public async Task<ActionResult<int>> GetTodaysArticleCount(string author)
    {
        var count = await _articleRepository.GetTodaysArticleCount(author);
        return Ok(count);
    }
}

