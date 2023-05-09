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

    public async Task AddToBlacklist(BlackList blackList)
    {

        var existingBlacklist = await _dbContext.BlackList
            .FirstOrDefaultAsync(b => b.ArticleId == blackList.ArticleId  && b.UserId == blackList.UserId);

        if (existingBlacklist == null)
        {
            
            await _dbContext.BlackList.AddAsync(blackList);
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

    public async Task<List<BlackList>> GetBlacklistedByArticleId(int articleId)
    {
        return await _dbContext.BlackList.Where(p => p.ArticleId == articleId).ToListAsync();
    }

    public async Task DeleteBlaclist(BlackList Blacklist)
    {
        _dbContext.BlackList.Remove(Blacklist); 
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsArticleBlacklistedForUser(int articleId, int userId)
    {
        var result = await _dbContext.BlackList
            .FirstOrDefaultAsync(x => x.ArticleId == articleId && x.UserId == userId);

        return result != null;
    }
}