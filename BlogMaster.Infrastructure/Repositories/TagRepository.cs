using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure.Repositories;

public class TagRepository:ITagRepository
{
    private readonly AppDbContext _dbContext;

    public TagRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Tag>> GetAllTags()
    {
        return await _dbContext.Tag.ToListAsync();
    }

    public async Task<Tag> GetTagById(int tagId)
    {
        return await _dbContext.Tag.FindAsync(tagId);
    }

    public async Task<List<Tag>> GetTagsByArticleId(int articleId)
    {
        return await _dbContext.Tag.Where(t => t.ArticleTags.Any(at => at.ArticleId == articleId)).ToListAsync();
    }

    public async Task<Tag> AddTag(Tag tag)
    {
          _dbContext.Tag.Add(tag);
        await _dbContext.SaveChangesAsync();
        return tag;
    }

    public async Task UpdateTag(Tag tag)
    {
        _dbContext.Tag.Update(tag);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTag(int tagId)
    {
        var tagToRemove = await _dbContext.Tag.FindAsync(tagId);
        if (tagToRemove != null)
        {
            _dbContext.Tag.Remove(tagToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}