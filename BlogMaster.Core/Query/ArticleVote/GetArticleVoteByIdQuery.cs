using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.ArticleVote;

public class GetArticleVoteByIdQuery : IRequest<Entity.ArticleVote>
{
    public int Id { get; set; }
}
public class GetArticleVoteByIdQueryHandler : IRequestHandler<GetArticleVoteByIdQuery, Entity.ArticleVote>
{
    private readonly IArticleVoteRepository _articleVoteRepository;
    private readonly ILogger<GetArticleVoteByIdQueryHandler> _logger;
    
    public GetArticleVoteByIdQueryHandler(IArticleVoteRepository articleVoteRepository,
                ILogger<GetArticleVoteByIdQueryHandler> logger)
    {
        _articleVoteRepository = articleVoteRepository;
        _logger = logger;
    
    }
    public async Task<Entity.ArticleVote> Handle(GetArticleVoteByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message:$"{request.Id} ArticleVote came");
        var result = await _articleVoteRepository.GetById(request.Id);
        if (result==null)
        {
            //  throw new ArticleNotFoundException($"article not found articleId: {request.Id}");
        }
    
        return result;
    }
}
        
