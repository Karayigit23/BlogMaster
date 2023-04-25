using BlogMaster.Core.Entity;
namespace BlogMaster.Core.InterFaces;


public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategori();
    Task<List<Article>> GetArticlesByCategoryId(int categoryId);
    Task<Category> GetCategoryById(int categoryId);
    Task<Category> AddCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(Category category);
}