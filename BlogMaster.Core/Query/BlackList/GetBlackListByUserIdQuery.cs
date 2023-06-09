using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.BlackList;

public class GetBlackListByUserIdQuery:IRequest<List<Entity.BlackList>>
{
    public int UserId { get; set; }
    
}

public class GetBlackListByUserIdQueryHandler : IRequestHandler<GetBlackListByUserIdQuery, List<Entity.BlackList>>
{
    private readonly IBlacklistRepository _blacklistRepository;
    private readonly ILogger<GetBlackListByUserIdQueryHandler> _logger;

    public GetBlackListByUserIdQueryHandler(IBlacklistRepository blacklistRepository,
        ILogger<GetBlackListByUserIdQueryHandler> logger)
    {
        _blacklistRepository = blacklistRepository;
        _logger = logger;
    }
    public async Task<List<Entity.BlackList>> Handle(GetBlackListByUserIdQuery request, CancellationToken cancellationToken)
    {
       _logger.LogInformation(message:$"{request.UserId}all articles belonging to this user id have arrived");
       var result = await _blacklistRepository.GetBlacklistByUserId(request.UserId);
       if (result==null || result.Any())
       {
           throw new NotFoundException($"blacklist not found userId:{request.UserId}");
       }

       return result;
    }
}