using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Tag;

public class GetAllTagQuery:IRequest<List<Entity.Tag>>
{
    public class GetAllTagQueryHandler:IRequestHandler<GetAllTagQuery,List<Entity.Tag>>
    {
        private readonly ITagRepository _TagRepository;
        private readonly ILogger<GetAllTagQueryHandler> _logger;

        public GetAllTagQueryHandler(ITagRepository tagRepositoryi, ILogger<GetAllTagQueryHandler> logger)
        {
            _TagRepository = tagRepositoryi;
            _logger = logger;
        }
        
        public async Task<List<Entity.Tag>> Handle(GetAllTagQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message:"All the Tag have came");
            return await _TagRepository.GetAllTags();
            
        }
    }
}