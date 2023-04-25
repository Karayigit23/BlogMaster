using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IArticleRepository
{
    
    Task<Article> GetArticleById(int id);
    Task<List<Article>> GetArticlesByCategory(string category);
    Task<List<Article>> GetArticlesByTag(string tagId);
    Task<List<Article>> GetArticlesByAuthor(string author);
   
    Task<List<Article>> GetAllArticles();
    Task AddArticle(Article article);
    Task UpdateArticle(Article article);
    Task DeleteArticle(Article article);
    Task<int> GetTodaysArticleCount(string requestAuthor);
   
}