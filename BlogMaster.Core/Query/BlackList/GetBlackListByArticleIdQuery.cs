using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.BlackList;

public class GetBlackListByArticleIdQuery:IRequest<List<Entity.BlackList>>
{
    public int ArticleId { get; set; }
}

public class GetBlackListByArticleIdQueryHandler : IRequestHandler<GetBlackListByArticleIdQuery, List<Entity.BlackList>>
{
    private readonly IBlacklistRepository _blacklistRepository;
    private readonly ILogger<GetBlackListByArticleIdQueryHandler> _logger;

    public GetBlackListByArticleIdQueryHandler(IBlacklistRepository blacklistRepository,
        ILogger<GetBlackListByArticleIdQueryHandler> logger)
    {
        _blacklistRepository = blacklistRepository;
        _logger = logger;
    }
    public async Task<List<Entity.BlackList>> Handle(GetBlackListByArticleIdQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation(message:$"{request.ArticleId}All users belonging to this article id came");
      var result = await _blacklistRepository.GetBlacklistedByArticleId(request.ArticleId);
      if (result==null|| result.Count==0)
      {
          throw new NotFoundException($"blacklist not found articleId:{request.ArticleId}");
      }

      return result;
    }
}