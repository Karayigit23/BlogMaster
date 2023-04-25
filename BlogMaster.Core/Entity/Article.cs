namespace BlogMaster.Core.Entity;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
    public int CategoryId { get; set; }
    public List<Comment> Comments { get; set; }
    public List<Tag> Tags { get; set; }
    public Category Category { get; set; }
    public Tag Tag { get; set; }
    
    public User AuthorUser { get; set; }
    
    //hem user hem strng author olmamalı gibi
}