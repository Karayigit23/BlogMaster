using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IBlacklistRepository
{
    Task<List<BlackList>>  GetAll();
    Task AddToBlacklist(BlackList blackList);
    Task<BlackList> GetBlacklistById(int id);
    
    Task<List<BlackList>> GetBlacklistByUserId(int userId);

    Task<List<BlackList>> GetBlacklistedByArticleId(int articleId);

    Task DeleteBlaclist(BlackList Blacklist);
}