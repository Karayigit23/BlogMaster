using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Category;

public class CreateCategoryCommand: IRequest<Entity.Category>
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Entity.Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Entity.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Entity.Category
        {
            Name = request.Name,
            Description = request.Description,
            Articles = new List<Article>() 
        };
        await _categoryRepository.AddCategory(category);
        return category;
    }
}