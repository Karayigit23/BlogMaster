namespace BlogMaster.Core.Entity;

public class BlackList
{
    
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public DateTime BlacklistDate { get; set; }
    
}