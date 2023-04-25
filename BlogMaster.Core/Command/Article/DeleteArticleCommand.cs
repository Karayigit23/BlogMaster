using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.ArticleCommand;

public class DeleteArticleCommand:IRequest
{
    public int Id { get; set; }
}

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
{
    private readonly IArticleRepository _articleRepository;

    public DeleteArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    //EĞER BULAMAZSA HATA FIRLAT
    public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var Article = await _articleRepository.GetArticleById(request.Id);
       
        await _articleRepository.DeleteArticle(Article);
    }
}