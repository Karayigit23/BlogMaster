using BlogMaster.Core.Command.ArticleCommand;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using MediatR;
using Moq;

namespace BlogMaster.Test.Command.Article;

public class DeleteArticleCommandHandlerTest
{
    private Mock<IArticleRepository> _mockArticleRepository;
    private DeleteArticleCommandHandler _handler;

    public DeleteArticleCommandHandlerTest()
    {
        _mockArticleRepository = new Mock<IArticleRepository>();
        _handler = new DeleteArticleCommandHandler(_mockArticleRepository.Object);
    }

    [SetUp]
    public void Setup()
    {
        _mockArticleRepository.Invocations.Clear();
    }

    [Test]
    public async Task DeleteArticleCommandHandler_ShouldDeleteArticle_WhenArticleExists()
    {
        // Arrange
        var articleId = 1;
        var article = new Core.Entity.Article { Id = articleId };
        _mockArticleRepository
            .Setup(repo => repo.GetArticleById(articleId))
            .ReturnsAsync(article); // set mock response

        // Act
        var command = new DeleteArticleCommand { Id = articleId };
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockArticleRepository.Verify(repo => repo.DeleteArticle(article), Times.Once);
        Assert.AreEqual(Unit.Value, result);
    }

    [Test]
    public void DeleteArticleCommandHandler_ShouldThrowException_WhenArticleDoesNotExist()
    {
        // Arrange
        var command = new DeleteArticleCommand
        {
            Id = 1
        };
        _mockArticleRepository
            .Setup(repo => repo.GetArticleById(It.IsAny<int>()))
            .ReturnsAsync((Core.Entity.Article)null); 
        var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.AreEqual($"not found{command.Id}", ex.Message);
    }

  






}