using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure.Repositories;

public class CommentRepository:ICommentRepository
{
    private readonly AppDbContext _dbContext;

    public CommentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Comment>> GetAllComments()
    {
        return await _dbContext.Comment.ToListAsync();
    }

    public async Task<Comment> GetCommentById(int commentId)
    {
        return await _dbContext.Comment.FindAsync(commentId);
    }

    public async Task<List<Comment>> GetCommentsByArticleId(int articleId)
    {
        return await _dbContext.Comment.Where(c => c.ArticleId == articleId).ToListAsync();
    }

    public async Task<Comment> AddComment(Comment comment)
    {
         _dbContext.Comment.Add(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task UpdateComment(Comment comment)
    {
        _dbContext.Comment.Update(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteComment(int commentId)
    {
        var commentToRemove = await _dbContext.Comment.FindAsync(commentId);
        if (commentToRemove != null)
        {
            _dbContext.Comment.Remove(commentToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}