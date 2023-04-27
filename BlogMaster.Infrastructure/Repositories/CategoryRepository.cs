using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure.Repositories;

public class CategoryRepository:ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Category>> GetAllCategori()
    {
        return await _dbContext.Category.ToListAsync();
    }

    public async Task<Category> GetCategoryById(int categoryId)
    {
        return await _dbContext.Category.FindAsync(categoryId);
    }

    public async Task<Category> AddCategory(Category category)
    {
        _dbContext.Category.Add(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }
    public async Task<List<Article>> GetArticlesByCategoryId(int categoryId)
    {
        return await _dbContext.Article.Where(a => a.CategoryId == categoryId).ToListAsync();
    }

    public async Task UpdateCategory(Category category)
    {
       _dbContext.Set<Category>().Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategory(Category category)
    {
     
        _dbContext.Category.Remove(category); 
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategory(int categoryId)
    {
    }
}