using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.ArticleCommand;

public class UpdateArticleCommand:IRequest<Article>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    
}

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Article>
{
    private readonly IArticleRepository _articleRepository;

    public UpdateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    
    public async Task<Article> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleById(request.Id);

        article.Title = request.Title;
        article.Content = request.Title;
        article.PublishDate = request.PublishDate;

        await _articleRepository.UpdateArticle(article);
        return article;

    }
}