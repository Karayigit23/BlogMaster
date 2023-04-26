namespace BlogMaster.Core.Entity;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Article> Articles { get; set; }
    public List<ArticleTag> ArticleTags { get; set; } 
}