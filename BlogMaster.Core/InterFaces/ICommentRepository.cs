using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllComments();
    Task<Comment> GetCommentById(int commentId);
    Task<List<Comment>> GetCommentsByArticleId(int articleId);
    Task<Comment> AddComment(Comment comment);
    Task UpdateComment(Comment comment);
    Task DeleteComment(int commentId);
    
}