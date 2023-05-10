using BlogMaster.Core.Exception;
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
   
    public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var Article = await _articleRepository.GetArticleById(request.Id);
        if (Article==null)
        {
            throw new NotFoundException($"not found{request.Id}");
        }
       
        await _articleRepository.DeleteArticle(Article);
        return Unit.Value;
    }
}