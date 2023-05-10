using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Article;

public class GetArticlesByCategoryQuery:IRequest<List<Entity.Article>>
{
    public int CategoryId { get; set; }
}

public class GetArticlesByCategoryQueryHandler : IRequestHandler<GetArticlesByCategoryQuery,List< Entity.Article>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ILogger<GetArticlesByCategoryQueryHandler> _logger;

    public GetArticlesByCategoryQueryHandler(IArticleRepository articleRepository,
        ILogger<GetArticlesByCategoryQueryHandler> logger)
    {
        _articleRepository = articleRepository;
        _logger = logger;
    }
    
   
    public async Task<List<Entity.Article>> Handle(GetArticlesByCategoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message:$"{request.CategoryId} ArticleCategory came");
        var result = await _articleRepository.GetArticlesByCategory(request.CategoryId);
        if (result==null)
        {
                
            throw new NotFoundException($"article not found articleId: {request.CategoryId}");
        }
        return result;

    }
}


