using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.BlackList;

public class CreateBlackListCommand:IRequest<Entity.BlackList>
{
    public int ArticleId { get; set; }
    public int UserId { get; set; }
    public DateTime BlacklistDate { get; set; }
}

public class CreateBlackListCommandHandler : IRequestHandler<CreateBlackListCommand, Entity.BlackList>
{
    private readonly IBlacklistRepository _blacklistRepository;

    public CreateBlackListCommandHandler(IBlacklistRepository blacklistRepository)
    {
        _blacklistRepository = blacklistRepository;
    }
    
    public async Task<Entity.BlackList> Handle(CreateBlackListCommand request, CancellationToken cancellationToken)
    {
        var isBlacklisted = await _blacklistRepository.IsArticleBlacklistedForUser(request.ArticleId, request.UserId);
        if (isBlacklisted)
        {
            throw new ControlException("This user has already been added to this article.");
        }

        var Blacklist = new Entity.BlackList
        {
            ArticleId = request.ArticleId,
            UserId = request.UserId,
            BlacklistDate = DateTime.Now
        };
        await _blacklistRepository.AddToBlacklist(Blacklist);
        return Blacklist;
    }
}