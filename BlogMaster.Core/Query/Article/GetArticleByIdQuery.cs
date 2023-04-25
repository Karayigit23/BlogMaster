using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlogMaster.Core.Query.Article
{

    public class GetArticleByIdQuery :IRequest<Entity.Article>
    {
        public int Id { get; set; }
    }
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, Entity.Article>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<GetArticleByIdQueryHandler> _logger;

        public GetArticleByIdQueryHandler(IArticleRepository articleRepository,
            ILogger<GetArticleByIdQueryHandler> logger)
        {
            _articleRepository = articleRepository;
            _logger = logger;

        }
        public async Task<Entity.Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message:$"{request.Id} Article came");
            var result = await _articleRepository.GetArticleById(request.Id);
            if (result==null)
            {
                
              //  throw new ArticleNotFoundException($"car not found carId: {request.Id}");
            }

            return result;
        }
    }
}