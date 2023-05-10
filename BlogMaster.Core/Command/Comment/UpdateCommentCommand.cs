using System.Globalization;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Comment;

public class UpdateCommentCommand :IRequest<Entity.Comment>
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public string Author { get; set; }
    
  
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Entity.Comment>
{
    private readonly ICommentRepository _commentRepository;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<Entity.Comment> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment =  await _commentRepository.GetCommentById(request.Id);
        if (comment==null)
        {
            throw new NotFoundException($"not found {request.Id}");
        }

        comment.Content = request.Content;
        comment.PublishDate=DateTime.Now;
        comment.Author = request.Author;
      
       
      

        await _commentRepository.UpdateComment(comment);
        return comment;
    }
}