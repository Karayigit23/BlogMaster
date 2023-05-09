using BlogMaster.Core.InterFaces;

using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Tag;

public class GetTagsByArticleIdQuery:IRequest<List<Entity.Tag>>
{
    public int ArticleId { get; set; }
}

public class GetTagsByArticleIdQueryHandler : IRequestHandler<GetTagsByArticleIdQuery, List<Entity.Tag>>
{
    private readonly ITagRepository _tagRepository;
    private readonly ILogger<GetTagsByArticleIdQueryHandler> _logger;

    public GetTagsByArticleIdQueryHandler(ITagRepository tagRepository, ILogger<GetTagsByArticleIdQueryHandler> logger)
    {
        _tagRepository = tagRepository;
        _logger = logger;
    }

    public async Task<List<Entity.Tag>> Handle(GetTagsByArticleIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message: $"{request.ArticleId} article came");
        var result = await _tagRepository.GetTagsByArticleId(request.ArticleId);
        if (result== null || result.Count==0)
            {

                throw new Exception($"user not found articleId: {request.ArticleId}");
          
            }
        
       

        return result;
    }
}





