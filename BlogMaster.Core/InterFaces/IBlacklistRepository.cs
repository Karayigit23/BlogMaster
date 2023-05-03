using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IBlacklistRepository
{
    Task<List<BlackList>>  GetAll();
    Task AddToBlacklist(int articleId, int userId);
    Task<BlackList> GetBlacklistById(int id);
    
    Task<List<BlackList>> GetBlacklistByUserId(int userId);

    Task<List<BlackList>> GetBlacklistedUsersByArticleId(int articleId);

    Task DeleteBlaclistItem(BlackList Blacklist);
}