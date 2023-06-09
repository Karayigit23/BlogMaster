using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;

namespace BlogMaster.Core.Command.Category;

public class DeleteCategoryCommand:IRequest
{
    public int Id { get; set; }
}
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
   
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var Category = await _categoryRepository.GetCategoryById(request.Id);
        if (Category==null)
        {
            throw new NotFoundException($"not found {request.Id}");
        }
       
        await _categoryRepository.DeleteCategory(Category);
        return Unit.Value;
    }
}