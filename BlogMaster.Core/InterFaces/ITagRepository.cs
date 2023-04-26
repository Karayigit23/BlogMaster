using BlogMaster.Core.Entity;

namespace BlogMaster.Core.InterFaces;

public interface ITagRepository
{
    Task<List<Tag>> GetAllTags();
    Task<Tag> GetTagById(int tagId);
    Task<List<Tag>> GetTagsByArticleId(int articleId);
    Task<Tag> AddTag(Tag tag);
    Task UpdateTag(Tag tag);
    Task DeleteTag(Tag tag);
}