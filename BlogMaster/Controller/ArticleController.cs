using BlogMaster.Core.Command.ArticleCommand;
using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogMaster.Controller;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    
    private readonly IMediator _mediator;

    // Inject the ArticleRepository via constructor injection
    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET api/article
    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetAllArticles()
    {
        var articles = await _mediator.Send(new GetAllArticleQuery());
        return Ok(articles);
    }

    // GET api/article/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticleById(int id)
    {
        
        
        var article = await _mediator.Send(request: new GetArticleByIdQuery{Id = id});
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

        var query = new SearchArticleQuery {Id = id,Keyword = keyword,CategoryId = categoryId,TagId = tagId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET api/article/category/categoryName
    [HttpGet("category/{category}")]
    public async Task<ActionResult<List<Article>>> GetArticlesByCategory(int category)
    {

        var query = new GetArticlesByCategoryQuery {CategoryId = category };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    

    // POST api/article
    [HttpPost]
    public async Task<ActionResult> AddArticle(Article article)
    {
        await _mediator.Send(article);
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

        article.Id = id;
        await _mediator.Send(article);
        return Ok();
    }

    // DELETE api/article/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteArticle(int id)
    {
        var query = new DeleteArticleCommand { Id = id };
        var result=await _mediator.Send(query);
        return Ok(result);
    }

}

