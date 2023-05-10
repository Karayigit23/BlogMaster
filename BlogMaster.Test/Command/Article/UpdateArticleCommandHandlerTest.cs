using BlogMaster.Core.Command.ArticleCommand;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Article;

public class UpdateArticleCommandHandlerTest
{
    private Mock<IArticleRepository> _mockArticleRepository;
        private UpdateArticleCommandHandler _handler;

        public UpdateArticleCommandHandlerTest()
        {
            _mockArticleRepository = new Mock<IArticleRepository>();
            _handler = new UpdateArticleCommandHandler(_mockArticleRepository.Object);
        }

        [SetUp]
        public void Setup()
        {
            _mockArticleRepository.Invocations.Clear();
        }

        [Test]
        public async Task UpdateArticleCommandHandler_ShouldUpdateArticle_WhenInputIsValid()
        {
            // Arrange
            var command = new UpdateArticleCommand
            {
                Id = 1,
                Title = "Updated Test Article",
                Content = "This is an updated test article.",
                PublishDate = DateTime.Now,
            };
            var article = new Core.Entity.Article
            {
                Id = 1,
                Title = "Test Article",
                Content = "This is a test article.",
                PublishDate = DateTime.Now.AddDays(-1),
                CategoryId = 1,
                UserId = 1,
                UserName = "testuser"
            };
            _mockArticleRepository
                .Setup(repo => repo.GetArticleById(command.Id))
                .ReturnsAsync(article);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockArticleRepository.Verify(repo => repo.UpdateArticle(article), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(command.Title, result.Title);
            Assert.AreEqual(command.Content, result.Content);
            Assert.AreEqual(command.PublishDate, result.PublishDate);
            Assert.AreEqual(article.CategoryId, result.CategoryId);
            Assert.AreEqual(article.UserId, result.UserId);
            Assert.AreEqual(article.UserName, result.UserName);
        }

        [Test]
        public void UpdateArticleCommandHandler_ShouldThrowException_WhenArticleDoesNotExist()
        {
            // Arrange
            var command = new UpdateArticleCommand
            {
                Id = 1,
                Title = "Updated Test Article",
                Content = "This is an updated test article.",
                PublishDate = DateTime.Now,
            };
            _mockArticleRepository
                .Setup(repo => repo.GetArticleById(command.Id))
                .ReturnsAsync((Core.Entity.Article)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.AreEqual($"Article not found for id: {command.Id}", ex.Message);
        }
    }


