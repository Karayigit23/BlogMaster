using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IArticleRepository
{
    
    Task<Article> GetArticleById(int id);
    
    Task<List<Article>> GetArticlesByCategory(int categoryId);
    Task<List<Article>> GetArticlesByTag(string tagId);
    Task<List<Article>> GetArticlesByAuthor(string author);
   
    Task<List<Article>> GetAllArticles();
    Task AddArticle(Article article);
    Task UpdateArticle(Article article);
    Task DeleteArticle(Article article);
    Task<int> GetTodaysArticleCount(string requestAuthor);
    Task<List<Article>> Search(int? id, string? keyword, int? categoryId, int? tagId);
}