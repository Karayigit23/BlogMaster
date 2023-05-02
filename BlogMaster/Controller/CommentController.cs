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
        public async Task<ActionResult<List<Comment>>> GetAllComments()
        {
            var comments = await _mediator.Send(new GetAllCommentQuery());
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById(int id)
        {
            var comment = await _mediator.Send(request: new GetCommentByIdQuery { Id = id });
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpGet("article/{articleId}")]
        public async Task<ActionResult<List<Comment>>> GetCommentsByArticleId(int articleId)
        {
            var comments = await _mediator.Send(request: new GetCommentsByArticleIdQuery { ArticleId = articleId });
            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody] Comment comment)
        {
            await _mediator.Send(comment);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            comment.Id = id;
            await _mediator.Send(comment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task DeleteComment(int id)
        {
            await _mediator.Send(new DeleteCommentCommand { Id = id });
        }
    }
}
