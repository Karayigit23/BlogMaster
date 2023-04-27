using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure.Repositories;

public class ArticleVoteRepository:IArticleVoteRepository
{
    private readonly AppDbContext _dbContext;
    
    public ArticleVoteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddVote(ArticleVote articleVote)
    {
        _dbContext.ArticleVote.Add(articleVote);
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task UpdateVote(ArticleVote articleVote)
    {
        _dbContext.ArticleVote.Update(articleVote);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ArticleVote> GetById(int id)
    {
        return await _dbContext.ArticleVote.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<ArticleVote>> GetUserVotes(int userId)
    {
        return await _dbContext.ArticleVote.Where(a => a.UserId==userId ).ToListAsync();
    }
}