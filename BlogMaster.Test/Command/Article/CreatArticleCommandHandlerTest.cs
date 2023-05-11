using BlogMaster.Core.Command.ArticleCommand;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Article;

public class CreatArticleCommandHandlerTest
{
    [TestFixture]
public class CreateArticleHandlerTests
{
    private Mock<IArticleRepository> _mockArticleRepository;
    private CreateArticleHandler _handler;

    public CreateArticleHandlerTests()
    {
        _mockArticleRepository = new Mock<IArticleRepository>();
        _handler = new CreateArticleHandler(_mockArticleRepository.Object);
    }

    [SetUp]
    public void Setup()
    {
        _mockArticleRepository.Invocations.Clear();
    }

    [Test]
    public async Task CreateArticleHandler_ShouldAddArticle_WhenInputIsValid()
    {
        // Arrange
        var command = new CreateArticleCommand
        {
            Title = "Test Article",
            Content = "This is a test article.",
            PublishDate = DateTime.Now,
            CategoryId = 1,
            UserId = 1,
            UserName = "testuser"
        };
        _mockArticleRepository
            .Setup(repo => repo.GetTodaysArticleCount(It.IsAny<int>()))
            .ReturnsAsync(1); // set mock response

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockArticleRepository.Verify(repo => repo.AddArticle(It.IsAny<Core.Entity.Article>()), Times.Once);
        Assert.IsNotNull(result);
        Assert.AreEqual(command.Title, result.Title);
        Assert.AreEqual(command.Content, result.Content);
        Assert.AreEqual(command.PublishDate, result.PublishDate);
        Assert.AreEqual(command.CategoryId, result.CategoryId);
        Assert.AreEqual(command.UserId, result.UserId);
        Assert.AreEqual(command.UserName, result.UserName);
    }

    [Test]
    public void CreateArticleHandler_ShouldThrowException_WhenUserPublishesMoreThanTwoArticles()
    {
        // Arrange
        var command = new CreateArticleCommand
        {
            Title = "Test Article",
            Content = "This is a test article.",
            PublishDate = DateTime.Now,
            CategoryId = 1,
            UserId = 1,
            UserName = "testuser"
        };
        _mockArticleRepository
            .Setup(repo => repo.GetTodaysArticleCount(It.IsAny<int>()))
            .ReturnsAsync(2); // set mock response

        // Act & Assert
        var ex = Assert.ThrowsAsync<CountException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.AreEqual("You cannot publish more than 2 articles today.", ex.Message);
    }
}

}