
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
        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoryQuery());
            return categories;
        }

        [HttpGet("{id}")]
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _mediator.Send(request: new GetCategoryByIdQuery { Id = id });
            
            return category;
        }
        

        [HttpPost]
        public async Task AddCategory([FromBody]CreateCategoryCommand category)
        {
           await _mediator.Send(category);
           
        }

        [HttpPut("{id}")]
        public async Task UpdateCategory(int id,[FromBody] UpdateCategoryCommand category)
        {
            category.Id = id;
            await _mediator.Send(category);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand { Id = id });
            
        }
    }
}
