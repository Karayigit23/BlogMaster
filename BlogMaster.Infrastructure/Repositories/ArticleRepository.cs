using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure.Repositories;

public class ArticleRepository:IArticleRepository
{
  
    

    private readonly AppDbContext _dbContext;
    
    public ArticleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    //bu arama ile ilgili tüm kısımları search kısmına at articldan ayır  
    public async Task<Article> GetArticleById(int id)
    {
        return await _dbContext.Article.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Article>> GetArticlesByCategory(string category)
    {
         return await _dbContext.Article.Where(a => a.Category.Name == category).ToListAsync();
    }
    public async Task<List<Article>> GetArticlesByTag(string tag)
    {
        return await _dbContext.Article.Where(a => a.Tag.Name == tag).ToListAsync();

    }

    public async Task<List<Article>> GetArticlesByAuthor(string author)
    {
        return await _dbContext.Article.Where(a => a.AuthorUser.UserName == author).ToListAsync();
    
    }

   

    public async Task<List<Article>> GetAllArticles()
    {
        return await _dbContext.Article.ToListAsync();
    }

    public async Task AddArticle(Article article)
    {
       _dbContext.Article.Add(article);
        await _dbContext.SaveChangesAsync();
      
    }

    public async Task UpdateArticle(Article article)
    {
        _dbContext.Article.Update(article);
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task DeleteArticle(Article article)
    {


        _dbContext.Article.Remove(article); 
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task<int> GetTodaysArticleCount(string requestAuthor)
    {
        DateTime today = DateTime.UtcNow.Date;
        DateTime tomorrow = today.AddDays(1);
        
        int count = await _dbContext.Article.Where(a => a.AuthorUser.UserName == requestAuthor && a.PublishDate >= today && a.PublishDate < tomorrow).CountAsync();

        return count;
    }
}

