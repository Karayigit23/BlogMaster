using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Category;

public class UpdateCategoryCommand : IRequest<Entity.Category>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Entity.Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Entity.Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryById(request.Id);
        if (category==null)
        {
            throw new NotFoundException($"not found {request.Id}");
        }

        category.Name = request.Name;
        category.Description = request.Description;
      

        await _categoryRepository.UpdateCategory(category);
        return category;

    }
}
    
