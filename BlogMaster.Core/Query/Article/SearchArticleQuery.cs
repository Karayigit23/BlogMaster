using BlogMaster.Core.InterFaces;
using MediatR;
using Microsoft.Extensions.Logging;


namespace BlogMaster.Core.Query.Article
{
    public class SearchArticleQuery : IRequest<List<Entity.Article>>
    {
        public int? Id { get; set; }
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
        public int? TagId { get; set; }

        public class SearchArticleQueryHandler : IRequestHandler<SearchArticleQuery, List<Entity.Article>>
        {
            private readonly IArticleRepository _articleRepository;
            private readonly ILogger<SearchArticleQueryHandler> _logger;

            public SearchArticleQueryHandler(IArticleRepository articleRepository, ILogger<SearchArticleQueryHandler> logger)
            {
                _articleRepository = articleRepository;
                _logger = logger;
            }

            public async Task<List<Entity.Article>> Handle(SearchArticleQuery request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Searching for articles");
                var result =
                    await _articleRepository.Search(request.Id, request.Keyword, request.CategoryId, request.TagId);
                if (result == null)
                {
                    //  throw new ArticleNotFoundException($"article not found articleId: {request.Id}");
                }

                return result;
            }
        }
    }
}
