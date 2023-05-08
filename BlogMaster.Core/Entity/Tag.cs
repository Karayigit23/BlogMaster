namespace BlogMaster.Core.Entity;

public class Tag:EntityBase
{
   
    public string Name { get; set; }
    public List<ArticleTag> ArticleTags { get; set; }
}