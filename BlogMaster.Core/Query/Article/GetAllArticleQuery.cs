using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Article;

public class GetAllArticleQuery : IRequest<List<Entity.Article>>
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public class GetAllArticleQueryHandler:IRequestHandler<GetAllArticleQuery,List<Entity.Article>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<GetAllArticleQueryHandler> _logger;

        public GetAllArticleQueryHandler(IArticleRepository articleRepository,
            ILogger<GetAllArticleQueryHandler> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }
        
        public async Task<List<Entity.Article>> Handle(GetAllArticleQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message:"All the Article have came");
            return await _articleRepository.GetAllArticles(request.Page,request.Size);
            
        }
    }
    
