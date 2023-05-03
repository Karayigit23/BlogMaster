using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.BlackList;

public class GetBlackListByIdQuery:IRequest<Entity.BlackList>
{
    public int Id { get; set; }
}

public class GetBlackListByIdQueryHandler : IRequestHandler<GetBlackListByIdQuery, Entity.BlackList>
{
    private readonly IBlacklistRepository _blacklistRepository;
    private readonly ILogger<GetBlackListByIdQueryHandler> _logger;

    public GetBlackListByIdQueryHandler(IBlacklistRepository blacklistRepository,
        ILogger<GetBlackListByIdQueryHandler> logger)
    {
        _blacklistRepository = blacklistRepository;
        _logger = logger;
    }
    public async Task<Entity.BlackList> Handle(GetBlackListByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message:$"{request.Id}black item came");
        var result = await _blacklistRepository.GetBlacklistById(request.Id);
        if (result==null)
        {
            //throw new BlacklistNotFoundException($"Blacklist item not found Id:{request.Id}");
        }

        return result;
    }
}