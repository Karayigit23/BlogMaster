using BlogMaster.Core.Command.Comment;
using BlogMaster.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Comment;
using MediatR;

namespace BlogMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _mediator.Send(new GetAllCommentQuery());
            return comments;
        }

        [HttpGet("{id}")]
        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _mediator.Send(request: new GetCommentByIdQuery { Id = id });
            
            return comment;
        }

        [HttpGet("article/{articleId}")]
        public async Task<List<Comment>> GetCommentsByArticleId(int articleId)
        {
            var comments = await _mediator.Send(request: new GetCommentsByArticleIdQuery { ArticleId = articleId });
            return comments;
        }

        [HttpPost]
        public async Task AddComment([FromBody] Comment comment)
        {
            await _mediator.Send(comment);
           
        }

        [HttpPut("{id}")]
        public async Task UpdateComment(int id, [FromBody] Comment comment)
        {
          

            comment.Id = id;
            await _mediator.Send(comment);
            
        }

        [HttpDelete("{id}")]
        public async Task DeleteComment(int id)
        {
            await _mediator.Send(new DeleteCommentCommand { Id = id });
        }
    }
}
