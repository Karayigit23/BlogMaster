namespace BlogMaster.Core.Entity;

public class ArticleTag:EntityBase
{
    public int ArticleId { get; set; } 
    public int TagId { get; set; } 

    
    public Article Article { get; set; }


    public Tag Tag { get; set; }
}