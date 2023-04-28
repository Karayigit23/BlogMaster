using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Comment;

public class GetCommentsByArticleIdQuery:IRequest<List<Entity.Comment>>
{
    public int ArticleId { get; set; }
}

public class GetCommentsByArticleIdQueryHandler : IRequestHandler<GetCommentsByArticleIdQuery,List< Entity.Comment>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly Logger<GetCommentsByArticleIdQueryHandler> _logger;

    public GetCommentsByArticleIdQueryHandler(ICommentRepository commentRepository,
        Logger<GetCommentsByArticleIdQueryHandler> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }

    public async Task<List<Entity.Comment>> Handle(GetCommentsByArticleIdQuery request, CancellationToken cancellationToken)
    {
        
        _logger.LogInformation(message: $"{request.ArticleId} article came");
       var result = await _commentRepository.GetCommentsByArticleId(request.ArticleId);
        if (result == null || result.Any() )
        {

            //  throw new commandNotFoundException($"user not found articleId: {request.articleId}");
            return null;
        }

        return result;
    }
}



