using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Comment;

public class CreateCommentCommand : IRequest<Entity.Comment>
{
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public string Author { get; set; }
    public int UserId { get; set; }
  
    public int ArticleId { get; set; }
}
public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Entity.Comment>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Entity.Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Entity.Comment
        {
            Content = request.Content,
            PublishDate = request.PublishDate,
            Author = request.Author,
            UserId = request.UserId,
            
            ArticleId = request.ArticleId
        };

        await _commentRepository.AddComment(comment);

        return comment;
    }
}
