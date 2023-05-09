namespace BlogMaster.Core.Entity;

public class Comment:EntityBase
{
   
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    
    
    public string Author { get; set; }
    public int UserId { get; set; }
    public string Article { get; set; }
    public int ArticleId { get; set; }
}