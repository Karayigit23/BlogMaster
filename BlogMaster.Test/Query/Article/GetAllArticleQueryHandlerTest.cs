using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Article;

public class GetAllArticleQueryHandlerTest
{
    private Mock<ILogger<GetAllArticleQueryHandler>> _loggerMock;
        private Mock<IArticleRepository> _articleRepositoryMock;
        private GetAllArticleQueryHandler _handler;

        public GetAllArticleQueryHandlerTest()
        {
            _loggerMock = new Mock<ILogger<GetAllArticleQueryHandler>>();
            _articleRepositoryMock = new Mock<IArticleRepository>();
            _handler = new GetAllArticleQueryHandler(_articleRepositoryMock.Object, _loggerMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            _articleRepositoryMock.Invocations.Clear();
        }

        [Test]
        public async Task GetAllArticleQueryHandler_ShouldReturnListOfArticles()
        {
            // Arrange
            var query = new GetAllArticleQuery { Page = 1, Size = 10 };
            var articles = new List<Core.Entity.Article> { new Core.Entity.Article { Id = 1, Title = "Article 1", Content = "Content 1", PublishDate = DateTime.Now }, new Core.Entity.Article { Id = 2, Title = "Article 2", Content = "Content 2", PublishDate = DateTime.Now } };
            _articleRepositoryMock.Setup(x => x.GetAllArticles(query.Page, query.Size)).ReturnsAsync(articles);

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Core.Entity.Article>>(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Article 1", result[0].Title);
            Assert.AreEqual("Content 2", result[1].Content);
          
        }
    }

