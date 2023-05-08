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
    public  Task<Article> GetArticleById(int id)
    {
        return  _dbContext.Article.Where(a => a.Id == id).FirstOrDefaultAsync();
    }

    public  Task<List<Article>> GetArticlesByCategory(int categoryId)
    {
        return  _dbContext.Article.Where(a => a.CategoryId == categoryId).ToListAsync();
    }

    public Task<List<Article>> GetAllArticles(int page, int size)
    {
       var articles=   _dbContext.Article.Skip((page-1)*size).Take(size).ToListAsync();
       if (articles == null)
       {
           throw new Exception();
       }

       return articles;

    }

    public  Task<List<Article>> Search(int? id,string? keyword,int? categoryId,int? tagId, int page, int size)
    {
        var query = _dbContext.Article.AsQueryable();
        if (id != null)
        {
            query = query.Where(p => p.Id == id);
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(p =>
                p.Title.Contains(keyword) || p.Content.Contains(keyword) || p.UserName.Contains(keyword));
        }

        if (categoryId != null)
        {
            query = query.Where(p => p.CategoryId == categoryId);
        }

        if (tagId != null)
        {
            query = query.Where(p => p.ArticleTags.Any(t => t.TagId == tagId));
        }

        return query.OrderByDescending(x => x.PublishDate).Skip((page - 1) * size).Take(size).ToListAsync();
       
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

    public async Task<int> GetTodaysArticleCount(int requestUserId)
    {
        DateTime today = DateTime.UtcNow.Date;
        DateTime tomorrow = today.AddDays(1);
        
        int count = await _dbContext.Article.Where(a => a.UserId == requestUserId && a.PublishDate >= today && a.PublishDate < tomorrow).CountAsync().ConfigureAwait(false);

        return count;
    }


  
}

