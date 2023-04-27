using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface IArticleVoteRepository
{
    Task AddVote(ArticleVote articleVote);
    Task UpdateVote(ArticleVote articleVote);
    Task<ArticleVote> GetById(int id);
    Task<List<ArticleVote>> GetUserVotes(int userId);
}