using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Comment;

public class DeleteCommentCommand:IRequest
{
    public int Id { get; set; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
     
    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetCommentById(request.Id);
        await _commentRepository.DeleteComment(comment);
        return Unit.Value;
    }
}