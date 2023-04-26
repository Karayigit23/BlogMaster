using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Comment;

public class GetCommentByIdQuery:IRequest<Entity.Comment>
{
    public int Id { get; set; }
}
public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Entity.Comment>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger<GetCommentByIdQueryHandler> _logger;

    public GetCommentByIdQueryHandler(ICommentRepository commentRepository,
        ILogger<GetCommentByIdQueryHandler> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;

    }
    public async Task<Entity.Comment> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message:$"{request.Id} Comment came");
        var result = await _commentRepository.GetCommentById(request.Id);
        if (result==null)
        {
                
            //  throw new ArticleNotFoundException($"car not found carId: {request.Id}");
        }

        return result;
    }
}