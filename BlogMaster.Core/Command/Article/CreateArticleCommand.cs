using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.ArticleCommand;

public class CreateArticleCommand:IRequest<Article>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public int CategoryId { get; set; }
    public string User { get; set; }
    
}

public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Article>
{
    private readonly IArticleRepository _articleRepository;
    
    public CreateArticleHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;

    }  
    
    
    
    //burada hata çıkma olasılığı çok yüksek
    public async Task<Article> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        
        int todaysArticleCount = await _articleRepository.GetTodaysArticleCount(request.User);

       
        if (todaysArticleCount >= 2)
        {
            throw new Exception("You cannot publish more than 2 articles today.");
        }

      
        var article = new Article
        {
            Title = request.Title,
            Content = request.Content,
            PublishDate = request.PublishDate,
            CategoryId = request.CategoryId,
            UserName = request.User
        };

    
        await _articleRepository.AddArticle(article);

        return article;
    }
}