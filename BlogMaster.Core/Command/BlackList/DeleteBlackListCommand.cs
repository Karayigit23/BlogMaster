using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.BlackList;

public class DeleteBlackListCommand:IRequest
{
    public int Id { get; set; }
}

public class DeleteBlackListCommandHandler : IRequestHandler<DeleteBlackListCommand>
{
    private readonly IBlacklistRepository _blacklistRepository;

    public DeleteBlackListCommandHandler(IBlacklistRepository blacklistRepository)
    {
        _blacklistRepository = blacklistRepository;
    }
    public async Task<Unit> Handle(DeleteBlackListCommand request, CancellationToken cancellationToken)
    {
        var list = await _blacklistRepository.GetBlacklistById(request.Id);
        if (list == null)
        {
            throw new NotFoundException($"not found {request.Id}");
        }
        await _blacklistRepository.DeleteBlaclist(list);
        return Unit.Value;
    }
}