using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Comment;

public class GetAllCommentQuery:IRequest<List<Entity.Comment>>
{
    public class GetAllCommentQueryHandler:IRequestHandler<GetAllCommentQuery,List<Entity.Comment>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<GetAllCommentQueryHandler> _logger;
        
        public async Task<List<Entity.Comment>> Handle(GetAllCommentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message:"All the Comment have came");
            return await _commentRepository.GetAllComments();
            
        }
    }
}

    
