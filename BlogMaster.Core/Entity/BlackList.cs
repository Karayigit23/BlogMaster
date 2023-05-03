namespace BlogMaster.Core.Entity;

public class BlackList:EntityBase
{
    
  
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public DateTime BlacklistDate { get; set; }
    
}