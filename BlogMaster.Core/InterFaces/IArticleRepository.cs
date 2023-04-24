using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IArticleRepository
{
    
    Task<Article> GetArticleById(int id);
    Task<List<Article>> GetArticlesByCategory(string category);
    Task<List<Article>> GetArticlesByTag(string tag);
    Task<List<Article>> GetArticlesByAuthor(string author);
    Task<List<Article>> GetArticlesByKeyword(string keyword);
    Task<List<Article>> GetAllArticles();
    Task AddArticle(Article article);
    Task UpdateArticle(Article article);
    Task DeleteArticle(int id);
}