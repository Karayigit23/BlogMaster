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
    private readonly ILogger<GetCommentsByArticleIdQueryHandler> _logger;

    public GetCommentsByArticleIdQueryHandler(ICommentRepository commentRepository,
        ILogger<GetCommentsByArticleIdQueryHandler> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }

    public async Task<List<Entity.Comment>> Handle(GetCommentsByArticleIdQuery request, CancellationToken cancellationToken)
    {
        
        _logger.LogInformation(message: $"{request.ArticleId} article came");
       var result = await _commentRepository.GetCommentsByArticleId(request.ArticleId);
       if (result == null)
       {
           throw new Exception("Error: comment result is null.");
       }

       if (!result.Any())
       {
           throw new Exception();
       }
      




        return result;
    }
}



