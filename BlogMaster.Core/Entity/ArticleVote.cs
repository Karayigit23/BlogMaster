namespace BlogMaster.Core.Entity;

public class ArticleVote:EntityBase
{
  
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public bool Like { get; set; }
    public bool Dislike { get; set; }
    
} 