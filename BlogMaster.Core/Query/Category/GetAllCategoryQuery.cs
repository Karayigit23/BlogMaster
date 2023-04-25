using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Category;

public class GetAllCategoryQuery:IRequest<List<Entity.Category>>
{
    public class GetAllCategoryQueryHandler:IRequestHandler<GetAllCategoryQuery,List<Entity.Category>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetAllCategoryQueryHandler> _logger;
        
        public async Task<List<Entity.Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message:"All the Category have came");
            return await _categoryRepository.GetAllCategori();
            
        }
    }
}