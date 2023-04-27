namespace BlogMaster.Core.Entity;

public class ArticleVote
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public bool Like { get; set; }
    public bool Dislike { get; set; }
    
} 