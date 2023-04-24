using BlogMaster.Core.Entity;
namespace BlogMaster.Core.InterFaces;


public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategori();
    Task<Category> GetCategoryById(int categoryId);
    Task<Category> AddCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(int categoryId);
}