using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure.Repositories;

public class BlackListRepository:IBlacklistRepository
{
    private readonly AppDbContext _dbContext;
    
    
    public BlackListRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BlackList>> GetAll()
    {
        return await _dbContext.BlackList.ToListAsync();
    }

    public async Task AddToBlacklist(int articleId, int userId)
    {

        var existingBlacklist = await _dbContext.BlackList
            .FirstOrDefaultAsync(b => b.ArticleId == articleId && b.UserId == userId);

        if (existingBlacklist == null)
        {
            // Create a new blacklist entry for the article and user
            var blacklist = new BlackList
            {
                ArticleId = articleId,
                UserId = userId
            };

            // Add the blacklist entry to the database
            await _dbContext.BlackList.AddAsync(blacklist);
            await _dbContext.SaveChangesAsync();

        }
        
    }

    public async Task<BlackList> GetBlacklistById(int id)
    {
        return await _dbContext.BlackList.FirstOrDefaultAsync(a => a.Id == id);

    }

    public async Task<List<BlackList>> GetBlacklistByUserId(int userId)
    {
        return await _dbContext.BlackList.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<List<BlackList>> GetBlacklistedUsersByArticleId(int articleId)
    {
        return await _dbContext.BlackList.Where(p => p.ArticleId == articleId).ToListAsync();
    }

    public async Task DeleteBlaclistItem(BlackList Blacklist)
    {
        _dbContext.BlackList.Remove(Blacklist); 
        await _dbContext.SaveChangesAsync();
    }
}