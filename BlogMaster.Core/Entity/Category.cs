namespace BlogMaster.Core.Entity;

public class Category:EntityBase
{
   
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Article> Articles { get; set; }
}