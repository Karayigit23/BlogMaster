using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<User>> GetAllUsers()
    {
        return await _dbContext.User.ToListAsync();
    }

    public async Task<User> GetUserById(int userId)
    {
        return await _dbContext.Set<User>().FindAsync(userId);
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<User> AddUser(User user)
    {
        _dbContext.User.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUser(User user)
    {  
        _dbContext.User.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        _dbContext.User.Remove(user); 
        await _dbContext.SaveChangesAsync();
    }
    
}








////kurallar bi adam bir gün için 2 den fazla bolg yazamasın
/// user black liste eklediği makaleyi ona hiç gözükmesi
/// beğeni yada beğenmedi reaksiyon