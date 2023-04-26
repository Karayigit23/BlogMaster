namespace BlogMaster.Core.Entity;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    
    public string UserName { get; set; }
    public int CategoryId { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Tag> Tags { get; set; }
    public Category Category { get; set; }
    
    public User User { get; set; }
    
    public List<ArticleTag> ArticleTags { get; set; } 
    
    //hem user hem strng author olmamalı gibi
}