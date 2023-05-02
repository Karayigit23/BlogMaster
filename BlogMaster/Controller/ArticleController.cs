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
    public async Task<List<Article>> GetAllArticles()
    {
        var articles = await _mediator.Send(new GetAllArticleQuery());
        return articles;
    }

    // GET api/article/{id}
    [HttpGet("{id}")]
    public async Task<Article> GetArticleById(int id)
    {
        
        
        var article = await _mediator.Send(request: new GetArticleByIdQuery{Id = id});
        
        return article;
    }
    

    // GET api/article/search?id=1&keyword=test&categoryId=2&tagId=3
    [HttpGet("search")]
    public async Task<List<Article>> Search(int? id, string? keyword, int? categoryId, int? tagId)
    {

        var query = new SearchArticleQuery {Id = id,Keyword = keyword,CategoryId = categoryId,TagId = tagId };
        var result = await _mediator.Send(query);
        return result;
    }

    // GET api/article/category/categoryName
    [HttpGet("category/{category}")]
    public async Task<List<Article>> GetArticlesByCategory(int category)
    {

        var query = new GetArticlesByCategoryQuery {CategoryId = category };
        var result = await _mediator.Send(query);
        return result;
    }

    

    // POST api/article
    [HttpPost]
    public async Task AddArticle(Article article)
    {
        await _mediator.Send(article);
        
    }

    // PUT api/article/5
    [HttpPut("{id}")]
    public async Task UpdateArticle(int id, Article article)
    {
       

        article.Id = id;
        await _mediator.Send(article);
       
    }

    // DELETE api/article/5
    [HttpDelete("{id}")]
    public async Task DeleteArticle(int id)
    {
        var query = new DeleteArticleCommand { Id = id };
        var result=await _mediator.Send(query);
      
    }

}

