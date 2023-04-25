using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Category;

public class GetCategoryByIdQuery:IRequest<Entity.Category>
{
    public int Id { get; set; }
}
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Entity.Category>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository,
        ILogger<GetCategoryByIdQueryHandler> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;

    }
    public async Task<Entity.Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message:$"{request.Id} Article came");
        var result = await _categoryRepository.GetCategoryById(request.Id);
        if (result==null)
        {
                
            //  throw new ArticleNotFoundException($"car not found carId: {request.Id}");
        }

        return result;
    }
}