namespace BlogMaster.Core.Entity;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public User Author { get; set; }
    public int UserId { get; set; }
    public Article Article { get; set; }
    public int ArticleId { get; set; }
}