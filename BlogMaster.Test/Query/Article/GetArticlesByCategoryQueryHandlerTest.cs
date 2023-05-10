using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Article;

public class GetArticlesByCategoryQueryHandlerTest
{
    private GetArticlesByCategoryQueryHandler _getArticlesByCategoryQueryHandler;
    private Mock<IArticleRepository> _articleRepositoryMock;
    private Mock<ILogger<GetArticlesByCategoryQueryHandler>> _loggerMock;

    public GetArticlesByCategoryQueryHandlerTest()
    {
        _articleRepositoryMock = new Mock<IArticleRepository>();
        _loggerMock = new Mock<ILogger<GetArticlesByCategoryQueryHandler>>();
        _getArticlesByCategoryQueryHandler = new GetArticlesByCategoryQueryHandler(_articleRepositoryMock.Object, _loggerMock.Object);
    }
    

    [SetUp]
        public void Setup()
        {
          _articleRepositoryMock.Invocations.Clear();
        }

        [Test]
        public async Task GetArticlesByCategoryQueryHandler_ShouldReturnArticles_WhenCategoryFound()
        {
            // Arrange
            var categoryId = 1;
            var articles = new List<Core.Entity.Article>()
            {
                new Core.Entity.Article() { Id = 1, CategoryId = categoryId },
                new Core.Entity.Article() { Id = 2, CategoryId = categoryId },
                new Core.Entity.Article() { Id = 3, CategoryId = categoryId },
            };
            _articleRepositoryMock.Setup(x => x.GetArticlesByCategory(categoryId)).ReturnsAsync(articles);

            // Act
            var query = new GetArticlesByCategoryQuery() { CategoryId = categoryId };
            var result = await _getArticlesByCategoryQueryHandler.Handle(query, new CancellationToken());

            // Assert
            result.Should().NotBeNull().And.BeOfType<List<Core.Entity.Article>>();
            result.Should().BeEquivalentTo(articles);
           
            _articleRepositoryMock.Verify(x => x.GetArticlesByCategory(categoryId), Times.Once);
        }

        [Test]
        public async Task GetArticlesByCategoryQueryHandler_ShouldReturnEmptyList_WhenCategoryNotFound()
        {
            // Arrange
            var categoryId = 1;
            List<Core.Entity.Article> articles =  new List<Core.Entity.Article>();
            _articleRepositoryMock.Setup(x => x.GetArticlesByCategory(categoryId)).ReturnsAsync(articles);

            // Act
            var query = new GetArticlesByCategoryQuery() { CategoryId = categoryId };
            var result = await _getArticlesByCategoryQueryHandler.Handle(query, new CancellationToken());

            // Assert
          
            result.Should().NotBeNull();
           
            _articleRepositoryMock.Verify(x => x.GetArticlesByCategory(categoryId), Times.Once);
        }
    }

