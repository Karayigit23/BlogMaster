using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Tag;

public class GetTagByIdQuery:IRequest<Entity.Tag>
{
    public int Id { get; set; }   
    
    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, Entity.Tag>
    {
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<GetTagByIdQueryHandler> _logger;
        public GetTagByIdQueryHandler(ITagRepository tagRepository, ILogger<GetTagByIdQueryHandler>
            logger)
        {
            _tagRepository = tagRepository;
            _logger = logger;
        }
        public async Task<Entity.Tag> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
  
            _logger.LogInformation(message:$"{request.Id} Tag came");
            var result = await _tagRepository.GetTagById(request.Id);
            if (result==null)
            {
                
                  throw new Exception($"tag not found tagId: {request.Id}");
            }

            return result;
        }
    }
}