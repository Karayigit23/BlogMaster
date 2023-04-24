using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;

namespace BlogMaster.Infrastructure.Repositories;

public class ArticleRepository:IArticleRepository
{
    private readonly DbContext _dbContext;
    
    public ArticleRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Article> GetArticleById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticlesByCategory(string category)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticlesByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticlesByAuthor(string author)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetArticlesByKeyword(string keyword)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetAllArticles()
    {
        throw new NotImplementedException();
    }

    public Task AddArticle(Article article)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArticle(Article article)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArticle(int id)
    {
        throw new NotImplementedException();
    }
}