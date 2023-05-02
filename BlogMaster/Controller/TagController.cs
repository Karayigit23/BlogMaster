using System.Threading.Tasks;
using BlogMaster.Core.Command.Tag;
using Microsoft.AspNetCore.Mvc;
using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Tag;
using MediatR;

namespace BlogMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Tag>> GetAllTags()
        {
            var tags = await _mediator.Send(new GetAllTagQuery());
            return tags;
        }

        [HttpGet("{id}")]
        public async Task<Tag> GetTagById(int id)
        {
            var tag = await _mediator.Send(request: new GetTagByIdQuery { Id = id });

            return tag;
        }

        [HttpGet("article/{articleId}")]
        public async Task<List<Tag>> GetTagsByArticleId(int articleId)
        {
            var tags = await _mediator.Send(request: new GetTagsByArticleIdQuery { ArticleId = articleId });
            return tags;
        }

        [HttpPost]
        public async Task AddTag([FromBody] Tag tag)
        {
            await _mediator.Send(tag);
        }

        [HttpPut("{id}")]
        public async Task UpdateTag(int id, [FromBody] Tag tag)
        {

            tag.Id = id;
            await _mediator.Send(tag);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTag(int id)
        {
            await _mediator.Send(new DeleteTagCommand { Id = id });
        }
    }
}
