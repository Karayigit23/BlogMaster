
using BlogMaster.Core.Command.ArticleVote;
using BlogMaster.Core.Entity;
using BlogMaster.Core.Query.ArticleVote;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleVoteController : ControllerBase
    { 
        private readonly  IMediator _mediator;

        public ArticleVoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddVote([FromBody] CreateArticleVoteCommand command)
        {
            await _mediator.Send(command);
            return Ok();
            
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVote(int id,[FromBody] UpdateArticleVoteCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return Ok();
            
        }

        [HttpGet("{id}")]
        public async Task<ArticleVote> GetById(int id)
        {
            var query = new GetArticleVoteByIdQuery { Id = id };
            return await _mediator.Send(query);
        }

        [HttpGet("user/{userId}")]
        public async Task<List<ArticleVote>> GetUserVotes(int userId)
        {
            var query = new GetArticleVotesByUserQuery { UserId = userId };
            return await _mediator.Send(query);
        }
    }
}
