using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IArticleRepository
{
    
    Task<Article> GetArticleById(int id);
    
    Task<List<Article>> GetArticlesByCategory(int categoryId);
    
    Task<List<Article>> GetAllArticles(int page,int size);
    Task AddArticle(Article article);
    Task UpdateArticle(Article article);
    Task DeleteArticle(Article article);
    Task<int> GetTodaysArticleCount(int requestUserId);
    Task<List<Article>> Search(int? id, string? keyword, int? categoryId, int? tagId,int page,int size);
}