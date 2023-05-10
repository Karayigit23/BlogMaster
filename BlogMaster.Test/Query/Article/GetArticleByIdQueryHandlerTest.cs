using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Article;

public class GetArticleByIdQueryHandlerTest
{
    private Mock<IArticleRepository> _articleRepositoryMock;
        private Mock<ILogger<GetArticleByIdQueryHandler>> _loggerMock;
        private IRequestHandler<GetArticleByIdQuery, Core.Entity.Article> _handler;

        public GetArticleByIdQueryHandlerTest()
        {
            _articleRepositoryMock = new Mock<IArticleRepository>();
            _loggerMock = new Mock<ILogger<GetArticleByIdQueryHandler>>();

            _handler = new GetArticleByIdQueryHandler(
                _articleRepositoryMock.Object, 
                _loggerMock.Object
            );
        }

        [SetUp]
        public void Setup()
        {
           _articleRepositoryMock.Invocations.Clear();
        }

        [Test]
        public async Task GetArticleByIdQueryHandler_ShouldReturnArticle_WhenArticleFound()
        {
            // Arrange
            var articleId = 1;
            var article = new Core.Entity.Article { Id = articleId };
            _articleRepositoryMock.Setup(x => x.GetArticleById(articleId))
                .ReturnsAsync(article);

            var query = new GetArticleByIdQuery { Id = articleId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.AreEqual(article, result);
            _articleRepositoryMock.Verify(x => x.GetArticleById(articleId), Times.Once);
            
        }

        [Test]
        public void GetArticleByIdQueryHandler_ShouldThrowException_WhenArticleNotFound()
        {
            // Arrange
            var articleId = 1;
            _articleRepositoryMock.Setup(x => x.GetArticleById(articleId))
                .ReturnsAsync((Core.Entity.Article)null);

            var query = new GetArticleByIdQuery { Id = articleId };

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () =>
                await _handler.Handle(query, CancellationToken.None)
            );

            Assert.AreEqual($"article not found articleId: {articleId}", ex.Message);
            _articleRepositoryMock.Verify(x => x.GetArticleById(articleId), Times.Once);
           
        }
    }


