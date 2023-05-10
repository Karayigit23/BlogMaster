using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.BlackList;

public class GetAllBlackListQuery:IRequest<List<Entity.BlackList>>
{
    public class GetallBlacklistQueryHandler:IRequestHandler<GetAllBlackListQuery,List<Entity.BlackList>>
    {
        private readonly IBlacklistRepository _blacklistRepository;
        private readonly ILogger<GetallBlacklistQueryHandler> _logger;

        public GetallBlacklistQueryHandler(IBlacklistRepository blacklistRepository,
            ILogger<GetallBlacklistQueryHandler> logger)
        {
            _blacklistRepository = blacklistRepository;
            _logger = logger;
        }
        public async Task<List<Entity.BlackList>> Handle(GetAllBlackListQuery request, CancellationToken cancellationToken)
        {
          _logger.LogInformation(message:"All the Blaclist have came");
          return await _blacklistRepository.GetAll();
        }
    }
}



