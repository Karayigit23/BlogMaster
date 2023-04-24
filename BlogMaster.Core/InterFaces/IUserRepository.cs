using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsers();
    Task<User> GetUserById(int userId);
    Task<User> GetUserByUsername(string username);
    Task<User> AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int userId);
}