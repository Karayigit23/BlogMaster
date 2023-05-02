
using BlogMaster.Core.Command.Category;
using Microsoft.AspNetCore.Mvc;
using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using BlogMaster.Core.Query.Category;
using MediatR;

namespace BlogMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoryQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _mediator.Send(request: new GetCategoryByIdQuery { Id = id });
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("{id}/articles")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesByCategoryId(int Categoryid)
        {
            var articles = await _mediator.Send(request: new GetArticlesByCategoryQuery { CategoryId = Categoryid });
            return Ok(articles);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            await _mediator.Send(category);
            return Ok();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id,[FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            category.Id = id;
            await _mediator.Send(category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand { Id = id });
            
        }
    }
}
